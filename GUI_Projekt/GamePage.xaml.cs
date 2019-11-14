/* Spielfeldseite. Realisiert ist das Spielfeld durch ein Canvas-Objekt. Auch die Steuerung
 * des Spiels findet hier statt. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private int gameArea_height;                //Spielfeldhöhe
        private int gameArea_width;                 //Spielfeldbreite
        private Ball NewBall;                       //Basketball
        private Arrow NewArrow;                     //Pfeil
        private DispatcherTimer BallTimer;          //Timer der Ball bewegt
        private DispatcherTimer ArrowAngleTimer;    //Timer der Ball bewegt
        private DispatcherTimer ArrowLengthTimer;   //Timer der Ball bewegt
        private Vector Speed;                       //Geschwindigkeit des Balls
        private Vector G_Factor;                     //Gravitationsstärke
        private bool gravity = false;               //Gravitation an/aus
        private const float attenuation = -0.75f;         //Elastizitätsverluste beim Aufprall
        private const float friction = 0.98f;             //Reibungsverluste
        private long tick = 0;                      //Timer"zeit"
        private int state = 0;                      //Spielzustand
        private const double arrowStartAngle = 0;         //Pfeilwinkel
        private const double arrowStartLength = 50;       //Pfeillänge
        private const double arrowAngleSpeed = 0.05;      //Pfeilwinkeländerungsgeschwindigkeit
        private const double arrowLengthSpeed = 0.05;     //Pfeilllängenänderungsgeschwindigkeit
        private const double arrowAngleAmplitude = Math.PI / 3;  //Pfeilwinkeländerungsamplitude
        private const double arrowLengthAmplitude = 20; //Pfeillängenänderungsamplitude
        private const double arrowSpeedFactor = 0.4;    //Proportionalitätsfaktor zwischen Pfeillänge und Ballgeschwindigkeit
        private Vector BallStartPosition;               //Startposition des Balls
        private Vector ArrowStartPosition;              //Startposition des Pfeils
        private int ballWidth;                          //Größe des Balls in Pixeln
        private Line NewArrowSkin;                      //ArrowSkin, später als Bild oder Polygon
        private Ellipse NewBallSkin;                    //Ballskin, später als Bild
        private SolidColorBrush ArrowStartColor;        //Anfangsfarbe des Pfeils
        
        /* Konstruktor vom Objekt GamePage, wird beim Aufrufen der Spielfeldseite ausgeführt */
        public GamePage()
        {
            InitializeComponent();
            gameArea_height = (int)GameArea.Height;     //Erhalte Werte für Spielfeldhöhe
            gameArea_width = (int)GameArea.Width;       //und -breite

            BallTimer = new DispatcherTimer();                  //Initialisiere BallTimer
            BallTimer.Interval = TimeSpan.FromMilliseconds(10); //BallTimer-Intervall
            BallTimer.Tick += ballTick;                         //BallTimer-Methode

            ArrowAngleTimer = new DispatcherTimer();            //wie oben
            ArrowAngleTimer.Interval = TimeSpan.FromMilliseconds(10);
            ArrowAngleTimer.Tick += arrowAngleTick;

            ArrowLengthTimer = new DispatcherTimer();           //wie oben
            ArrowLengthTimer.Interval = TimeSpan.FromMilliseconds(10);
            ArrowLengthTimer.Tick += arrowLengthTick;

            G_Factor = new Vector(0, 0.5);                  //Gravitationsfaktor, Richtung unten
            ballWidth = 50;                                 //Ballgröße
            BallStartPosition = new Vector(20, 200);        //Ballstartposition
            ArrowStartPosition = new Vector(80, 200 + ballWidth / 2);   //Pfeilstartposition

            gravity = true;             //Schalte Schwerkraft ein

            NewBallSkin = new Ellipse();        //neuer BallSkin
            NewBallSkin.Fill = Brushes.Black;   //Farbe schwarz
            NewBall = new Ball(GameArea, NewBallSkin, BallStartPosition, ballWidth);    //erstelle Ballobjekt
            NewBall.isVisible = true;       //Mache Ball auf Spielfeld sichtbar

            NewArrowSkin = new Line();          //neuer ArrowSkin
            ArrowStartColor = new SolidColorBrush(Color.FromRgb(127, 150, 127));    //Definiere Startfarbe
            NewArrowSkin.Stroke = ArrowStartColor;      //Gebe ArrowSkin die Startfarbe
            NewArrowSkin.StrokeThickness = 5;           //"Pfeil"dicke
            NewArrow = new Arrow(GameArea, NewArrowSkin, ArrowStartPosition, arrowStartLength, arrowStartAngle);    //erstelle Pfeilobjekt
            NewArrow.isVisible = true;      //Mache Pfeil auf Spielfeld sichtbar
        }

        /* Wird bei Mausklick aufgerufen, man kann später stattdessen ein Tastaturklick o.ä. nehmen*/
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (state)          //Reagiere je nach Zustand
            {
                case 0:             //Starte ArrowAngleTimer, d.h. ändere kontinuierlich Pfeilwinkel
                    tick = 0; state++; BallTimer.IsEnabled = false;
                    ArrowAngleTimer.IsEnabled = true; break;
                case 1:             //Starte ArrowLengthTimer, beende ArrowAngleTimer, d.h. behalte Winkel und ändere kontinuierlich Pfeillänge
                    tick = 0; state++; ArrowAngleTimer.IsEnabled = false;
                    ArrowLengthTimer.IsEnabled = true; break; ;
                case 2:             //Starte BallTimer, beende ArrowLengthTimer, d.h. behalte Länge und beginne Ballbewegung
                    tick = 0; state++; ArrowLengthTimer.IsEnabled = false;
                    NewArrow.isVisible = false;     //Verstecke Pfeil
                    Speed = NewArrow.Direction * arrowSpeedFactor;  //gebe Ball eine Startgeschwindigkeit proportional zur Pfeillänge und -richtung
                    BallTimer.IsEnabled = true; break;
                case 3:     //Setze alles auf Anfangszustand zurück
                    NewArrow.length = arrowStartLength;
                    NewArrow.angle = arrowStartAngle;
                    NewArrowSkin.Stroke = ArrowStartColor;
                    NewBall.Position = BallStartPosition;
                    NewArrow.isVisible = true;
                    BallTimer.IsEnabled = false;
                    tick = 0; state = 0; break;
                default: break;
            }
        }

        /* Methode, die Ball fliegen lässt */
        private void ballTick(object sender, EventArgs e)
        {
            collision();                // Reagiere auf mögliche Kollisionen
            gravitate();                // Ändere Ballgeschwindigkeit durch Gravitation
            NewBall.moveBall(Speed);    // Versetze Ball
        }

        /* Methode, die Pfeilwinkel kontinuierlich ändert */
        private void arrowAngleTick(object sender, EventArgs e)
        {
            tick++;     //"Zeit"variable
            NewArrow.angle = Math.Sin(arrowAngleSpeed * tick + arrowStartAngle) * arrowAngleAmplitude;
            /* Pfeilwinkel ist eine Sinusfunktion der "Zeit"variablen tick mit entsprechender Frequenz,
             * Amplitude und Startphase */
        }

        /* Methode, die Pfeillänge kontinuierlich ändert */
        private void arrowLengthTick(object sender, EventArgs e)
        {
            tick++;     //"Zeit"variable
            NewArrow.length = Math.Sin(arrowLengthSpeed * tick) * arrowLengthAmplitude + arrowStartLength;
            /* Pfeillänge ist eine Sinusfunktion der "Zeit"variablen tick mit entsprechender Frequenz,
             * Amplitude und Startphase */
            NewArrow.ArrowSkin.Stroke = new SolidColorBrush(Color.FromRgb((byte)(255*(Math.Sin(arrowLengthSpeed * tick)/2+0.5)), (byte)(150 * (Math.Cos(arrowLengthSpeed * tick * 2) / 2 + 0.5)), (byte)(255 * (Math.Sin(arrowLengthSpeed * tick) / (-2) + 0.5))));
            /* Pfeilfarbe wird ebenfalls über eine Sinusfunktion der "Zeit"variablen tick bestimmt.
             * Die Farbe schwankt dabei zwischen rot (255, 0, 0) und blau (0, 0, 255),
             * zwischendrin ist sie ein leicht grünlicher Grauton (127, 150, 127) */
        }

        /* Methode, die Gravitation simuliert */
        private void gravitate()
        {
            if (gravity)                //Wenn Gravitation eingeschaltet ist
                Speed += G_Factor;      //Gravitation verändert Geschwindigkeit linear (pro "Zeiteinheit" wird Speed dazuaddiert)
        }

        /* Methode zum Reagieren auf mögliche Kollisionen des Balls */
        private void collision()
        {
            /* Wenn Ball irgendwo abprallt, wird die zur Wand senkrechte
             * Geschwindigkeitskomponente durch attenuation gedämpft und umgekehrt
             * (da attenuation < 0), während die parallele Geschwindigkeitskomponente
             * durch friction gedämpft wird */
            gravity = true;
            if (NewBall.Position.Y + NewBall.ballSize + Speed.Y > gameArea_height)  //Kollision mit Boden
            {
                gravity = false;                //keine Gravitation wenn Ball am Boden liegt
                Speed.Y *= attenuation;
                Speed.X *= friction;
            }
            if (NewBall.Position.X + NewBall.ballSize + Speed.X > gameArea_width)   //Kollision mit rechter Wand
            {
                Speed.X *= attenuation;
                Speed.Y *= friction;
            }
            if (NewBall.Position.X + Speed.X < 0)                                   //Kollision mit linker Wand
            {
                Speed.X *= attenuation;
                Speed.Y *= friction;
            }
        }
    }
}