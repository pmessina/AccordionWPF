using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccordionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<Card, Bitmap> cardBitmaps = new Dictionary<Card, Bitmap>();
        Dictionary<Card, Bitmap> gameList = new Dictionary<Card, Bitmap>();
        Random generator = new Random(Environment.TickCount);

        private bool _isDown;
        private bool _isDragging;
        private System.Windows.Point _startPoint;
        private UIElement _realDragSource;
        private UIElement _dummyDragSource = new UIElement();
        KeyValuePair<Card, Bitmap> currentDrawCard;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            btnDrawCard.IsEnabled = false;

            //MainWindow1.Width = System.Windows.SystemParameters.WorkArea.Width;
            //MainWindow1.Height = System.Windows.SystemParameters.WorkArea.Height;

            wpGameBoard.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(stpGameBoard_PreviewMouseLeftButtonDown);
            wpGameBoard.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(stpGameBoard_PreviewMouseLeftButtonUp);
            wpGameBoard.PreviewMouseMove += new MouseEventHandler(stpGameBoard_PreviewMouseMove);
            wpGameBoard.DragEnter += new DragEventHandler(stpGameBoard_DragEnter);
            wpGameBoard.Drop += new DragEventHandler(stpGameBoard_Drop);
           
        }

        public void initCardBitmaps()
        {
            cardBitmaps.Add(new Card(1, 2), Properties.Resources.TwoC);
            cardBitmaps.Add(new Card(1, 3), Properties.Resources.ThreeC);
            cardBitmaps.Add(new Card(1, 4), Properties.Resources.FourC);
            cardBitmaps.Add(new Card(1, 5), Properties.Resources.FiveC);
            cardBitmaps.Add(new Card(1, 6), Properties.Resources.SixC);
            cardBitmaps.Add(new Card(1, 7), Properties.Resources.SevenC);
            cardBitmaps.Add(new Card(1, 8), Properties.Resources.EightC);
            cardBitmaps.Add(new Card(1, 9), Properties.Resources.NineC);
            cardBitmaps.Add(new Card(1, 10), Properties.Resources.TenC);
            cardBitmaps.Add(new Card(1, 11), Properties.Resources.JC);
            cardBitmaps.Add(new Card(1, 12), Properties.Resources.QC);
            cardBitmaps.Add(new Card(1, 13), Properties.Resources.KC);
            cardBitmaps.Add(new Card(1, 14), Properties.Resources.AC);

            cardBitmaps.Add(new Card(2, 2), Properties.Resources.TwoD);
            cardBitmaps.Add(new Card(2, 3), Properties.Resources.ThreeD);
            cardBitmaps.Add(new Card(2, 4), Properties.Resources.FourD);
            cardBitmaps.Add(new Card(2, 5), Properties.Resources.FiveD);
            cardBitmaps.Add(new Card(2, 6), Properties.Resources.SixD);
            cardBitmaps.Add(new Card(2, 7), Properties.Resources.SevenD);
            cardBitmaps.Add(new Card(2, 8), Properties.Resources.EightD);
            cardBitmaps.Add(new Card(2, 9), Properties.Resources.NineD);
            cardBitmaps.Add(new Card(2, 10), Properties.Resources.TenD);
            cardBitmaps.Add(new Card(2, 11), Properties.Resources.JD);
            cardBitmaps.Add(new Card(2, 12), Properties.Resources.QD);
            cardBitmaps.Add(new Card(2, 13), Properties.Resources.KD);
            cardBitmaps.Add(new Card(2, 14), Properties.Resources.AD);

            cardBitmaps.Add(new Card(3, 2), Properties.Resources.TwoH);
            cardBitmaps.Add(new Card(3, 3), Properties.Resources.ThreeH);
            cardBitmaps.Add(new Card(3, 4), Properties.Resources.FourH);
            cardBitmaps.Add(new Card(3, 5), Properties.Resources.FiveH);
            cardBitmaps.Add(new Card(3, 6), Properties.Resources.SixH);
            cardBitmaps.Add(new Card(3, 7), Properties.Resources.SevenH);
            cardBitmaps.Add(new Card(3, 8), Properties.Resources.EightH);
            cardBitmaps.Add(new Card(3, 9), Properties.Resources.NineH);
            cardBitmaps.Add(new Card(3, 10), Properties.Resources.TenH);
            cardBitmaps.Add(new Card(3, 11), Properties.Resources.JH);
            cardBitmaps.Add(new Card(3, 12), Properties.Resources.QH);
            cardBitmaps.Add(new Card(3, 13), Properties.Resources.KH);
            cardBitmaps.Add(new Card(3, 14), Properties.Resources.AH);

            cardBitmaps.Add(new Card(4, 2), Properties.Resources.TwoS);
            cardBitmaps.Add(new Card(4, 3), Properties.Resources.ThreeS);
            cardBitmaps.Add(new Card(4, 4), Properties.Resources.FourS);
            cardBitmaps.Add(new Card(4, 5), Properties.Resources.FiveS);
            cardBitmaps.Add(new Card(4, 6), Properties.Resources.SixS);
            cardBitmaps.Add(new Card(4, 7), Properties.Resources.SevenS);
            cardBitmaps.Add(new Card(4, 8), Properties.Resources.EightS);
            cardBitmaps.Add(new Card(4, 9), Properties.Resources.NineS);
            cardBitmaps.Add(new Card(4, 10), Properties.Resources.TenS);
            cardBitmaps.Add(new Card(4, 11), Properties.Resources.JS);
            cardBitmaps.Add(new Card(4, 12), Properties.Resources.QS);
            cardBitmaps.Add(new Card(4, 13), Properties.Resources.KS);
            cardBitmaps.Add(new Card(4, 14), Properties.Resources.AS);


        }

        public KeyValuePair<Card, Bitmap> draw()
        {
            KeyValuePair<Card, Bitmap> kvp = new KeyValuePair<Card, Bitmap>();

            if (cardBitmaps.Count > 0)
            {
                int randcard = generator.Next(cardBitmaps.Count);

                kvp = cardBitmaps.ElementAt(randcard);

                if (cardBitmaps.Contains(kvp))
                {
                    cardBitmaps.Remove(kvp.Key);
                }
                else
                {

                }
            }
            return kvp;

        }

        public class Card
        {
            public const int JACK = 11;
            public const int QUEEN = 12;
            public const int KING = 13;
            public const int ACE = 14;

            public const int CLUBS = 1;
            public const int DIAMONDS = 2;
            public const int HEARTS = 3;
            public const int SPADES = 4;

            private int value;
            private int suit;

            public Card(int cardSuit, int cardValue)
            {
                if (cardSuit < 1 || cardSuit > 4) throw new CardException("Invalid Suit");
                if (cardValue < 2 || cardValue > 14) throw new CardException("Invalid Value");

                value = cardValue;
                suit = cardSuit;

            }

            public enum Suit
            {
                Clubs = 1,
                Diamonds = 2,
                Hearts = 3,
                Spades = 4
            }

            // override object.Equals
            public override bool Equals(object obj)
            {
                Card card = obj as Card;

                if (this.getSuit() == card.getSuit())
                {
                    return true;
                }
                if (this.getValue() == card.getValue())
                {
                    return true;
                }

                return false;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public enum Value
            {
                Two = 2,
                Three = 3,
                Four = 4,
                Five = 5,
                Six = 6,
                Seven = 7,
                Eight = 8,
                Nine = 9,
                Ten = 10,
                Jack = 11,
                Queen = 12,
                King = 13,
                Ace = 14
            }

            public override string ToString()
            {
                return Enum.GetName(typeof(Value), value) + " of " + Enum.GetName(typeof(Suit), suit);
            }

            public int getSuit()
            {
                return suit;
            }

            public int getValue()
            {
                return value;
            }

        }

        public class CardException : Exception
        {

            public CardException(String s)
                : base(s)
            {

            }

        }

        private void btnInitGame_Click(object sender, RoutedEventArgs e)
        {
            initCardBitmaps();

            tbxDeckSize.Text = cardBitmaps.ToList().Count.ToString();

            KeyValuePair<Card, Bitmap> drawCard1 = draw();
            gameList.Add(drawCard1.Key, drawCard1.Value);

            //draws second random card as default
            KeyValuePair<Card, Bitmap> drawCard2 = draw();
            gameList.Add(drawCard2.Key, drawCard2.Value);

            foreach (KeyValuePair<Card, Bitmap> cardBitmap in gameList)
            {
                using (Bitmap bitmap = cardBitmap.Value)
                {
                    IntPtr hBitmap = bitmap.GetHbitmap();

                    try
                    {
                        BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                        System.Windows.Controls.Image img = new System.Windows.Controls.Image
                        {
                            Source = bs,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Width = 77,
                            Height = 101,
                            Margin = new Thickness(0, 0, 0, 0),
                            Tag = cardBitmap
                        };

                        wpGameBoard.Children.Add(img);
                    }

                    finally
                    {
                        DeleteObject(hBitmap);
                    }
                }
                btnInitGame.IsEnabled = false;
                btnDrawCard.IsEnabled = true;
            }

        }

        private void btnDrawCard_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<Card, Bitmap> drawCard1 = draw();

            if (drawCard1.Key == null)
            {
                MessageBoxResult mbr = MessageBox.Show("Deck is Empty", "No more cards to draw", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (mbr == MessageBoxResult.OK)
                {
                    //wpGameBoard.Children.Clear();
                    btnInitGame.IsEnabled = true;
                    btnDrawCard.IsEnabled = false;
                }
                if (mbr == MessageBoxResult.Cancel)
                {
                    this.Close();
                }
            }
            else
            {
                gameList.Add(drawCard1.Key, drawCard1.Value);

                using (Bitmap bitmap = drawCard1.Value)
                {
                    IntPtr hBitmap = bitmap.GetHbitmap();

                    try
                    {
                        BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                        System.Windows.Controls.Image img = new System.Windows.Controls.Image()
                        {
                            Source = bs,
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Width = 77,
                            Height = 101,
                            Margin = new Thickness(0, 0, 0, 0),
                            Tag = drawCard1
                        };

                        wpGameBoard.Children.Add(img);
                    }

                    finally
                    {
                        DeleteObject(hBitmap);
                    }

                    tbxDeckSize.Text = cardBitmaps.ToList().Count.ToString();
                }
            }

        }

        private void stpGameBoard_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source == this.wpGameBoard)
            {
            }
            else
            {
                _isDown = true;
                _startPoint = e.GetPosition(this.wpGameBoard);
            }
        }

        private void stpGameBoard_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDown = false;
            _isDragging = false;
            _realDragSource.ReleaseMouseCapture();
        }

        private void stpGameBoard_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDown)
            {
                if ((_isDragging == false) && ((Math.Abs(e.GetPosition(this.wpGameBoard).X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(this.wpGameBoard).Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                {
                    _isDragging = true;
                    _realDragSource = e.Source as UIElement;
                    object tag = (e.Source as System.Windows.Controls.Image).Tag;
                    KeyValuePair<Card, Bitmap> kvp = (KeyValuePair<Card, Bitmap>)tag;
                    currentDrawCard = kvp;
                    _realDragSource.CaptureMouse();
                    //Find card bitmap that matches image and get suit and value; store to compare with destination suit and value

                    DragDrop.DoDragDrop(_dummyDragSource, new DataObject("UIElement", e.Source, true), DragDropEffects.Move);
                }
            }
        }

        private void stpGameBoard_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("UIElement"))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

        private void stpGameBoard_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("UIElement"))
            {
                UIElement droptarget = e.Source as UIElement;
                int droptargetIndex = -1;
                int realDragSourceIndex = -1;
     
                for(int i = 0; i < this.wpGameBoard.Children.Count; i++)
                {
                    UIElement element = wpGameBoard.Children[i];

                    if (element.Equals(droptarget))
                    {
                        droptargetIndex = i;
                    }
                    else
                    {
                        if (element.Equals(_realDragSource))
                        {
                            realDragSourceIndex = i;
                        }
                    }
                }
                if (droptargetIndex != -1)
                {
                    //Make this smarter; add rules for executing this code
                    object tag = (e.Source as System.Windows.Controls.Image).Tag;
                    KeyValuePair<Card, Bitmap> kvp = (KeyValuePair<Card, Bitmap>)tag;
                     //droptargetIndex == (realDragSourceIndex - 3)
                    int targetSourceInterval = realDragSourceIndex-droptargetIndex;
                    if ((currentDrawCard.Key.getSuit() == kvp.Key.getSuit() || currentDrawCard.Key.getValue() == kvp.Key.getValue()) && (targetSourceInterval == 1 || targetSourceInterval == 3) )
                    {
                        this.wpGameBoard.Children.Remove(_realDragSource);
                        this.wpGameBoard.Children.RemoveAt(droptargetIndex);
                        this.wpGameBoard.Children.Insert(droptargetIndex, _realDragSource);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Move");
                    }
                }

                _isDown = false;
                _isDragging = false;
                _realDragSource.ReleaseMouseCapture();
            }
        }
    }


}

