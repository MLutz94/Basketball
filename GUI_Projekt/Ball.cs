//Klasse, welche den Ball im Spiel beschreibt.
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
    class Ball
    {
        private int _ballSize;          //Größe des Balls in Pixel 
        private Canvas _GameArea;       //Das Spielfeld als Canvas-Objekt
        private Ellipse _BallSkin;      //"Skin" des Balls, momentan ein Objekt vom Typ Ellipse, kann später durch ein Bild o.ä. ersetzt werden.
        private Vector _Position;       //Position des Balls auf dem Spielfeld (gemessen von links oben)
        private bool _isVisible;        //Boolean-Variable, welche Ball sichtbar macht/ihn versteckt

        /* Getter/Setter für _Position. Aktualisiert direkt die Position von _BallSkin auf dem Spielfeld*/
        public Vector Position
        {
            get { return _Position; }
            set { if(BallSkin != null)                      // BallSkin darf nicht leer sein
                {
                    _Position = value;
                    Canvas.SetTop(BallSkin, value.Y);       // Aktualisiere Position von
                    Canvas.SetLeft(BallSkin, value.X);      // _BallSkin auf dem Spielfeld
                }
            }
        }

        /* Getter/Setter für _GameArea */
        public Canvas GameArea { get => _GameArea; set => _GameArea = value; }

        /* Getter/Setter für _BallSkin */
        public Ellipse BallSkin { get => _BallSkin; set => _BallSkin = value; }

        /* Getter/Setter für _ballSize, achtet darauf, dass _ballSize größer 0 ist */
        public int ballSize
        {
            get { return _ballSize; }
            set { if (value > 0) _ballSize = value; }
        }

        /* Getter/Setter für _isVisible. Aktualisiert direkt die Sichtbarkeit von _BallSkin auf dem Spielfeld. */
        public bool isVisible
        { 
            get { return _isVisible; }
            set {
                _isVisible = value;
                if (BallSkin != null && GameArea != null)       //BallSkin und GameArea dürfen nicht leer sein
                {
                    if (value)
                    {
                        GameArea.Children.Add(BallSkin);        //Füge BallSkin dem Spielfeld hinzu
                    }
                    else
                    {
                        GameArea.Children.Remove(BallSkin);     //Entferne BallSkin vom Spielfeld
                    }
                }
            }
        }

        /* Konstruktor vom Objekt Ball */
        public Ball(Canvas GameArea, Ellipse BallSkin, Vector Position, int ballSize) //mit vorgegebener Position
        {
            this.BallSkin = BallSkin;       //Weise zuerst BallSkin zu, da andere Setter darauf zugreifen
            this.Position = Position;       //Benötigt BallSkin
            this.GameArea = GameArea;
            this.ballSize = ballSize;
            this.isVisible = false;         //Zum Schluss aufrufen, benötigt BallSkin und GameArea
            BallSkin.Width = ballSize;      //Passe Höhe und Breite von BallSkin an. Dies wird später
            BallSkin.Height = ballSize;     //etwas anders funktionieren wenn BallSkin ein Bild ist.
        }

        /* Bewegt Ball um Distance*/
        public void moveBall(Vector Distance)
        {
            Position += Distance;       //Ändere Position entsprechend, Setter sorgt dafür dass Ball auf dem Spielfeld auch direkt verschoben wird.
        }

    }
}
