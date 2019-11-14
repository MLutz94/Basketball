//Klasse, welche den Pfeil im Spiel beschreibt.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace GUI_Projekt
{
    class Arrow
    {
        private double _angle;          //Winkel des Pfeils in Radiant im mathematisch Positiven Sinn, Winkel 0 für x=1, y=0
        private double _length;         //Länge des Pfeils in Pixeln
        private Vector _Direction;      //Richtungsvektor des Pfeils
        private Canvas _GameArea;       //Das Spielfeld als Canvas-Objekt
        private Line _ArrowSkin;        //"Skin" des Pfeils, später Bild oder Pfeil als Polygon-Objekt
        private Vector _Position;       //Position des Pfeils (gemessen am unteren Pfeilende)
        private bool _isVisible;        //Boolean-Variable, welche Pfeil sichtbar macht/ihn versteckt

        /* Interne Funktion zum Aktualisieren der Positionen des Pfeilobjekts _ArrowSkin*/
        private void redraw()
        {
            ArrowSkin.X1 = Position.X;                      //X-Wert unteres Pfeilende
            ArrowSkin.Y1 = Position.Y;                      //Y-Wert unteres Pfeilende
            ArrowSkin.X2 = Position.X + Direction.X;        //X-Wert oberes Pfeilende
            ArrowSkin.Y2 = Position.Y + Direction.Y;        //Y-Wert oberes Pfeilende
        }

        /* Getter/Setter für _angle. Passt direkt _Direction an und aktualisiert _ArrowSkin-Position*/
        public double angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                _Direction.X = length * Math.Cos(_angle);
                _Direction.Y = -1 * length * Math.Sin(_angle);
                if (ArrowSkin != null)          //ArrowSkin darf nicht leer sein
                    redraw();
            }
        }

        /* Getter/Setter für _length. Passt direkt _Direction an und aktualisiert _ArrowSkin-Position*/
        public double length
        {
            get { return _length; }
            set
            {
                if (value > 0)
                {
                    _length = value;
                    _Direction.X = length * Math.Cos(_angle);
                    _Direction.Y = -1 * length * Math.Sin(_angle);
                    if (ArrowSkin != null)      //ArrowSkin darf nicht leer sein
                        redraw();
                }
            }
        }

        /* Getter/Setter für _Direction. Passt direkt _angle und _length an und aktualisiert _ArrowSkin-Position*/
        public Vector Direction
        {
            get { return _Direction; }
            set
            {
                _Direction = value;
                _length = _Direction.Length;
                _angle = Math.Atan(-1 * _Direction.Y / _Direction.X);
                if (ArrowSkin != null)          //ArrowSkin darf nicht leer sein
                    redraw();
            }
        }

        /* Getter/Setter für _GameArea */
        public Canvas GameArea { get => _GameArea; set => _GameArea = value; }

        /* Getter/Setter für _ArrowSkin */
        public Line ArrowSkin { get => _ArrowSkin; set => _ArrowSkin = value; }

        /* Getter/Setter für Position. Passt direkt Position von _ArrowSkin an*/
        public Vector Position
        {
            get { return _Position; }
            set
            {
                if (ArrowSkin != null)      //ArrowSkin darf nicht leer sein
                {
                    _Position = value;
                    redraw();
//                    Canvas.SetTop(ArrowSkin, value.Y);
//                    Canvas.SetLeft(ArrowSkin, value.X);
                }
            }
        }

        /* Getter/Setter für _isVisible. Aktualisiert direkt die Sichtbarkeit von _ArrowSkin auf dem Spielfeld. */
        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                if (ArrowSkin != null && GameArea != null)      //ArrowSkin und GameArea dürfen nicht leer sein
                {
                    if (value)
                    {
                        GameArea.Children.Add(ArrowSkin);       //Füge ArrowSkin dem Spielfeld hinzu
                    }
                    else
                    {
                        GameArea.Children.Remove(ArrowSkin);    //Entferne ArrowSkin vom Spielfeld
                    }
                }
            }
        }

        /* Konstruktor vom Objekt Arrow mit vorgegebenem Richtungsvektor */
        public Arrow(Canvas GameArea, Line ArrowSkin, Vector Position, Vector Direction) //mit vorgegebener Position
        {
            this.ArrowSkin = ArrowSkin;     //Weise zuerst ArrowSkin zu, da andere Setter darauf zugreifen
            this.Direction = Direction;     //Benötigt ArrowSkin
            this.Position = Position;       //Benötigt ArrowSkin
            this.GameArea = GameArea;
            this.isVisible = false;         //Zum Schluss aufrufen, benötigt ArrowSkin und GameArea
        }

        /* Konstruktor vom Objekt Arrow mit vorgegebener Länge und Winkel */
        public Arrow(Canvas GameArea, Line ArrowSkin, Vector Position, double length, double angle) //ohne vorgegebene Position
        {
            this.ArrowSkin = ArrowSkin;     //Weise zuerst ArrowSkin zu, da andere Setter darauf zugreifen
            this.length = length;           //Benötigt ArrowSkin
            this.angle = angle;             //Benötigt ArrowSkin
            this.Position = Position;       //Benötigt ArrowSkin
            this.GameArea = GameArea;
            this.isVisible = false;         //Zum Schluss aufrufen, benötigt ArrowSkin und GameArea
        }

    }
}
