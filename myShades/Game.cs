using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace myShades
{
    [DataContract]
    public class Game 
    {
        Canvas Board;
        Random RandomNumber = new Random();
        TextBlock ScoreBox;
        Gradient myColors = new Gradient();
        Rectangle[,] Blocks;
        TextBlock[,] TextBlocks;
        Rectangle NextColorRect;
        int[,] ColorsList;
        int[] CurentCoords = new int[2];  // Y : X
        int Score = 0;
        bool CureBlockAvailable =false;
        //bool IsGameOver = false ;
        public DispatcherTimer Timer ;
        double Speed = 300;              // milisec

        int ColumnCount = 5;
        int RowCount= 20;
        int BlockHeigh= 40;
        int BlockWidth = 100;

        //DataContractSerializer dcs = new DataContractSerializer(typeof(Game));

            
        public Game(Canvas board,TextBlock Score,Rectangle NextColor)
        {
            TextBlocks = new TextBlock[RowCount, ColumnCount];      // define textblocks list
            Blocks = new Rectangle[RowCount, ColumnCount];      // define blocks list
            ColorsList = new int[RowCount,ColumnCount];         // define colors list
            for (int i = 0; i < RowCount; i++)                  // set color list -1 (no color)
            {   
                for (int j = 0; j < ColumnCount; j++)
                {
                    ColorsList[i, j] = -1;
                }
            }
            Timer = new DispatcherTimer();                           // define timer
            Timer.Interval = TimeSpan.FromMilliseconds(Speed);       // set interval between ticks
            Timer.Tick += gameTick;                                  // add tick
            Timer.Start();                                           // start timer
            Board = board;                                           // define board
            Board.VerticalAlignment = VerticalAlignment.Bottom;      
            Board.HorizontalAlignment = HorizontalAlignment.Left;
            Board.Width = ColumnCount * BlockWidth;                  //set board width
            Board.Height= RowCount * BlockHeigh;                     //set board heidht
            ScoreBox = Score;                                        //define score textblock
            NextColorRect = NextColor;                               //define rect nextcolor
            NextColorRect.Width = BlockWidth;                           
            NextColorRect.Height = BlockHeigh;

            //GameOverMassage();
            //createBlockByNumber(79,2);

            //createBlock(0, 13, 0);
            //createBlock(1, 14, 0);
            //createBlock(2, 15, 0);
            //createBlock(3, 16, 0);
            //createBlock(4, 17, 0);
            //createBlock(5, 18, 0);
            //createBlock(6, 19, 0);

            //createBlock(1, 19, 0);createBlock(1, 19, 1);createBlock(4, 19, 2);createBlock(4, 19, 3);
            //createBlock(0, 18, 0);createBlock(0, 18, 1);createBlock(4, 18, 2);createBlock(4, 18, 3);
            //createBlock(6, 17, 0);createBlock(6, 17, 1);createBlock(6, 17, 2);
            //createBlock(0, 16, 0);createBlock(0, 16, 1);
            //createBlock(2, 15, 0);createBlock(2, 15, 1);
            //createBlock(3, 14, 0);createBlock(3, 14, 1);
        }


        public Game(Canvas board, TextBlock Score, Rectangle NextColor, int columnCount)
        {
            ColumnCount = columnCount;
            TextBlocks = new TextBlock[RowCount, ColumnCount];      // define textblocks list
            Blocks = new Rectangle[RowCount, ColumnCount];      // define blocks list
            ColorsList = new int[RowCount, ColumnCount];         // define colors list
            for (int i = 0; i < RowCount; i++)                  // set color list -1 (no color)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    ColorsList[i, j] = -1;
                }
            }
            Timer = new DispatcherTimer();                           // define timer
            Timer.Interval = TimeSpan.FromMilliseconds(Speed);       // set interval between ticks
            Timer.Tick += gameTick;                                  // add tick
            Timer.Start();                                           // start timer
            Board = board;                                           // define board
            Board.VerticalAlignment = VerticalAlignment.Bottom;
            Board.HorizontalAlignment = HorizontalAlignment.Left;
            Board.Width = ColumnCount * BlockWidth;                  //set board width
            Board.Height = RowCount * BlockHeigh;                     //set board heidht
            ScoreBox = Score;                                        //define score textblock
            NextColorRect = NextColor;                               //define rect nextcolor
            NextColorRect.Width = BlockWidth;
            NextColorRect.Height = BlockHeigh;

            //GameOverMassage();
            //createBlockByNumber(79,2);

            //createBlock(0, 13, 0);
            //createBlock(1, 14, 0);
            //createBlock(2, 15, 0);
            //createBlock(3, 16, 0);
            //createBlock(4, 17, 0);
            //createBlock(5, 18, 0);
            //createBlock(6, 19, 0);

            //createBlock(1, 19, 0);createBlock(1, 19, 1);createBlock(4, 19, 2);createBlock(4, 19, 3);
            //createBlock(0, 18, 0);createBlock(0, 18, 1);createBlock(4, 18, 2);createBlock(4, 18, 3);
            //createBlock(6, 17, 0);createBlock(6, 17, 1);createBlock(6, 17, 2);
            //createBlock(0, 16, 0);createBlock(0, 16, 1);
            //createBlock(2, 15, 0);createBlock(2, 15, 1);
            //createBlock(3, 14, 0);createBlock(3, 14, 1);
        }


        public void GameOverMassage()
        {
            StackPanel GameOver = new StackPanel();
            GameOver.Width = 1000;
            GameOver.Background = new SolidColorBrush(Colors.Gray);
            Board.Children.Add(GameOver);
            Canvas.SetTop(GameOver,9*BlockHeigh);
            
            GameOver.Children.Add(new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = "Game over\n You have "+Score+" scores \npress ESC to return to main menue"
            });
        }


        public void UpdateScoreBox(int score)
        {
            ScoreBox.Text = score+"";
        }


        public void Swap()
        {
            if (CureBlockAvailable)
            {
                int color = myColors.swapShade(ColorsList[CurentCoords[0], CurentCoords[1]]);
                ColorsList[CurentCoords[0], CurentCoords[1]] = color;

                NextColorRect.Fill = new SolidColorBrush(myColors.getShadesList()[myColors.getShadesQueue()[0]]);

                Blocks[CurentCoords[0], CurentCoords[1]].Fill = new SolidColorBrush(myColors.getShadesList()[ColorsList[CurentCoords[0], CurentCoords[1]]]);
            }
        }


        public int UpdateScore(int score)
        {
            Score += score;
            UpdateScoreBox(Score);
            return Score;
        }

        /// <summary>
        /// make block with his id
        /// </summary>
        /// <param name="color"></param>
        /// <param name="id"></param>
        public void createBlockByNumber(int color, int id)
        {
            int k = 0;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (id == k)
                    {
                        createBlock(color, i, j);
                        return;
                    }
                    k += 1;
                }
            }
        }


        /// <summary>
        /// make block with id color and rows and columns
        /// </summary>
        /// <param name="color"></param>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        public void createBlock(int color,int Row,int Column)
        {
            if (Blocks[Row, Column] == null)
            {
                ColorsList[Row, Column] = color;                 // set color index
                Blocks[Row, Column] = new Rectangle();           // crate new Rectengle
                Blocks[Row, Column].Fill = new SolidColorBrush(
                    myColors.getShadesList()[ColorsList[Row, Column]]); // set color for curent block from color list
                Blocks[Row, Column].Width = BlockWidth;         // set block width
                Blocks[Row, Column].Height = BlockHeigh;        // set block height 
                Board.Children.Add(Blocks[Row, Column]);        // add block on canvas
                Canvas.SetLeft(Blocks[Row, Column], Column*BlockWidth); // set left on canvaas
                Canvas.SetTop(Blocks[Row, Column], Row*BlockHeigh);
            }
            else
            {
                GameOverMassage();
                Timer.Stop();
            }
        }


        /// <summary>
        /// make block in first row and set fill for next color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int createBlock(int[] colors, int id)
        {
            CurentCoords[0] = 0;
            CurentCoords[1] = id;
            createBlock(colors[0], 0, id);
            NextColorRect.Fill = new SolidColorBrush(myColors.getShadesList()[colors[1]]);
            return id;
        }   


        /*public int createBlock(Color color,int id)
        {
            CurentCoords[0] = 0;
            CurentCoords[1] = id;
            ColorsList[CurentCoords[0], CurentCoords[1]] = myColors.getRandomShadeNumber();        // get random color index
            Blocks[CurentCoords[0], CurentCoords[1]] = new Rectangle();                            // crate new Rectengle
            Blocks[CurentCoords[0], CurentCoords[1]].Fill = new SolidColorBrush(
                myColors.getShadesList()[ColorsList[CurentCoords[0], CurentCoords[1]]]);            // set curent block color from color list
            Blocks[CurentCoords[0], CurentCoords[1]].Width = BlockWidth;                           // set block width
            Blocks[CurentCoords[0], CurentCoords[1]].Height = BlockHeigh;                          // set block height 
            Board.Children.Add(Blocks[CurentCoords[0], CurentCoords[1]]);                          // add block on canvas
            Canvas.SetLeft(Blocks[CurentCoords[0], CurentCoords[1]], CurentCoords[1] * BlockWidth);  // set left on canvaas
            Debug.WriteLine("Curent coord: " + CurentCoords[0] + ":" + CurentCoords[1]);
            return id;
        }*/

        /// <summary>
        /// move curent block aside
        /// </summary>
        /// <param name="dir"></param>
        public void moveCurentAside(int dir)
        {
            if ((dir == -1 && !CurentNeighbors()[0] || dir == 1 && !CurentNeighbors()[2]) 
                && Blocks[CurentCoords[0], CurentCoords[1]] != null && CureBlockAvailable&&
                Timer.IsEnabled)                                                                  
                                                                                                                  //if curent orientation -1 and no left neighbor OR curent orientatio 1 and no right neighbor
                                                                                                                  // AND curent block exist AND curent block available

            {
                Canvas.SetLeft(Blocks[CurentCoords[0], CurentCoords[1]], Canvas.GetLeft(Blocks[CurentCoords[0], CurentCoords[1]]) + BlockWidth*dir);  // move curent block left or right
                ColorsList[CurentCoords[0], CurentCoords[1] + dir] = ColorsList[CurentCoords[0], CurentCoords[1]];                                    // move curent color
                Blocks[CurentCoords[0], CurentCoords[1] + dir] = Blocks[CurentCoords[0], CurentCoords[1]];                                            // move crunt block in block list
                Blocks[CurentCoords[0], CurentCoords[1]] = null;                                                                                      // delete old block from block list  
                ColorsList[CurentCoords[0], CurentCoords[1]] = -1;                                                                                    // delete old color from colors list
                CurentCoords[1] += dir;                                                                                                               // udate X coord with directional
            }
           //Debug.WriteLine(this.CurentNeighbors());
        }

        /// <summary>
        /// move curent block down
        /// </summary>
        public void moveCurentDown()
        {
            if (!CurentNeighbors()[1] && Blocks[CurentCoords[0], CurentCoords[1]]!=null && Timer.IsEnabled)            // if no neighbors down and curent block exist (not null)
            {
                moveDown(CurentCoords[0], CurentCoords[1]);                                         // move down curent block
                CurentCoords[0] += 1;                                                               // update curent coords (+=1)
            }
        }

        /// <summary>
        /// mive specified block down 
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        public void moveDown(int Row,int Column)
        {
            if (Row < RowCount-1 && Column < ColumnCount)                  // if Row and Column in range
            {
                if (Blocks[Row, Column] != null) // if curent exist (not null) change curent top
                {
                    Canvas.SetTop(Blocks[Row, Column], Canvas.GetTop(Blocks[Row, Column]) + BlockHeigh);
                }
                ColorsList[Row + 1, Column] = ColorsList[Row, Column];                                  // move color in colors list down
                Blocks[Row + 1, Column] = Blocks[Row, Column];                                          // move block in blocks list down
                Blocks[Row, Column] = null;                                                             // delete old block
                ColorsList[Row, Column] = -1;                                                           // delete old color
            }
        }

        /// <summary>
        /// combine 2 block specified and block above
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        public void CombineBlocks(int Row, int Column)
        {
            Canvas.SetTop(Blocks[Row, Column], Canvas.GetTop(Blocks[Row, Column])+BlockHeigh);   // move curent block in next block
            Board.Children.Remove(Blocks[Row, Column]);                                          //delete old block from canvas
            ColorsList[Row, Column] = -1;                                                        //delete old color
            ColorsList[Row + 1, Column] += 1;                                                    //increase color in color list
            Blocks[Row, Column] = null;                                                          //delete old block from block list
            Blocks[Row + 1, Column].Fill = new SolidColorBrush(myColors.getShadesList()[ColorsList[Row + 1, Column]]); // new color
        }

        /// <summary>
        /// check selected row for same colors
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public bool CheckRow(int Row)
        {
            for (int i = 0; i < ColumnCount-1; i++)
            {
                if (ColorsList[Row, i + 1] != -1 && ColorsList[Row, i] != -1 &&
                    ColorsList[Row, i] == ColorsList[Row, i + 1])                           // if Block[Row,i] AND Block[Row,i+1] has same color AND that color exist
                {}
                else
                {
                    return false;
                }
            }
            return true;

        }

        //dont use
        public void DeleteRow(int Row)
        {
            for (int i = 0; i < ColumnCount ; i++)
            {
                ColorsList[Row, i] = -1;                            // delete color from colors list
                Board.Children.Remove(Blocks[Row, i]);              // remove from canvas
                Blocks[Row, i] = null;                              // delete from blocks list
            }
        }

        /// <summary>
        /// remove rectangles from canvas
        /// </summary>
        /// <param name="Row"></param>
        public void RemoveRowFromCanvas(int Row)
        {
            for (int i = 0; i < ColumnCount ; i++)
            {
                Board.Children.Remove(Blocks[Row, i]);
            }
        }

        /// <summary>
        /// move blocks from selected row down
        /// </summary>
        /// <param name="Row"></param>
        public void moveRow(int Row)
        {
            for (int i = 0; i < ColumnCount; i++)
            { 
                moveDown(Row,i);
            }
        }

        /// <summary>
        /// move selecte column down
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        public void moveColumn(int Row,int Column)
        {
            while (Row >= 0 && ColorsList[Row, Column] != -1)   // while Row in range AND colot exist (not -1)
            {
                moveDown(Row,Column);                           // move this block (Block[Row,Column]) down
                Row -= 1;                                       // decreas Row
            }
        }

        /// <summary>
        /// Combine all blocks in column if that need
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        public void UpdateColumn(int Row,int Column)
        {
            if (Row < RowCount && Column < ColumnCount)                                                                 // if column AND Row in range
            {
                while (Row<RowCount-1 && ColorsList[Row, Column] == ColorsList[Row + 1, Column] && 
                    ColorsList[Row , Column] != - 1 && ColorsList[Row + 1, Column] != myColors.getShadesCount() - 1 )   // while Row+1 in range AND curent and botomn blocks colors same AND colors exist
                {
                    UpdateScore(10);
                    CombineBlocks(Row, Column);                         // combine blocks
                    moveColumn(Row - 1, Column);                        // move bloks above down
                    Row += 1;                                           // increase Row
                }
                if (Row>0 && ColorsList[Row, Column] == ColorsList[Row - 1, Column] &&
                    ColorsList[Row - 1, Column] != myColors.getShadesCount() - 1)                               // if Row>0 AND curent and above blocks colors same and colors exist
                {
                    UpdateColumn(Row - 1, Column);                                                              // do all that again
                }
                if (CheckRow(Row))                      // if curent row (ROW) must be deleted
                {
                    UpdateRow(Row);                     // delete it 
                }
            }
        }

        //dont use
        public int getLastFillRow()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (Blocks[i, j] != null)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// move selected rows down
        /// </summary>
        /// <param name="FromRow"></param>
        /// <param name="ToRow"></param>
        public void moveRowsDown(int FromRow, int ToRow)
        {
            for (int i = FromRow; i >= ToRow; i--)
            {
                moveRow(i);
            }
        }

        /// <summary>
        /// combine all blocks in set Row if that need
        /// </summary>
        /// <param name="Row"></param>
        public void UpdateRow(int Row)
        {
                if (Row<RowCount)                               // Row in range
                {
                    RemoveRowFromCanvas(Row);                   // remove rectangles from canvas
                    moveRowsDown(Row - 1,0);                    // move rows above down
                    UpdateScore(100);
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        UpdateColumn(Row,i);                    // update all columns
                    }
                }
        }

        /// <summary>
        /// return neighbors of curent block in format  [left] [botomn] [right]
        /// </summary>
        /// <returns></returns>
        public bool[] CurentNeighbors()
        {
            bool[] Neighbors = { false, false, false }; 
            if (CurentCoords[1] ==0 || Blocks[CurentCoords[0], CurentCoords[1] - 1] != null)
            {
                Neighbors[0] = true;
            }
            if (CurentCoords[1] == ColumnCount-1 || Blocks[CurentCoords[0], CurentCoords[1] + 1] != null )
            {
                Neighbors[2] = true;
            }
            if (CurentCoords[0] == RowCount-1 || CurentCoords[0] < RowCount && Blocks[CurentCoords[0] + 1, CurentCoords[1]] != null )
            {
                Neighbors[1] = true;
            }
            //Debug.WriteLine("Neighbors: " + Neighbors[0] + "<- " + Neighbors[1] + " ->" + Neighbors[2]);
            return Neighbors;
        }

        /// <summary>
        /// main function 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void gameTick(object sender, object e)
        {
                if (!CureBlockAvailable) //curent block dont available 
                {
                    if (CheckRow(CurentCoords[0])) // if curent row must be deleted
                    {
                        UpdateRow(CurentCoords[0]); // delete it 
                    }
                    else
                    {
                        //this.createBlock(myColors.getRandomShade(), this.getRandomField());  // create block in first row
                        createBlock(myColors.getRandomShadeNumber(), getRandomField());
                        //NextColorRect.Fill = new SolidColorBrush(myColors.getShadesList()[ColorsList[Row, Column]]);
                        CureBlockAvailable = true;
                    }
                }
                else
                {
                    if (this.CurentNeighbors()[1]) // botom neighbor exist
                    {
                        CureBlockAvailable = false;
                        for (int i = CurentCoords[0]; i < RowCount - 1; i++) //combination blocks till it posible
                        {
                            if (ColorsList[i, CurentCoords[1]] == ColorsList[i + 1, CurentCoords[1]] &&
                                ColorsList[i + 1, CurentCoords[1]] != myColors.getShadesCount() - 1)     //if same colors and dont last color
                            {
                                UpdateScore(10);
                                CombineBlocks(CurentCoords[0], CurentCoords[1]);
                                CurentCoords[0] += 1;
                            }
                            else
                                break; //if can t combine 
                        }
                    }
                    else
                    {
                        this.moveCurentDown();
                    }
                }
                if (Speed > 75)
                    Timer.Interval = TimeSpan.FromMilliseconds(Speed - 10 * Score / 500);            
        }

        public void setColor(int c)
        {
            myColors.setColor(c);
        }

        //dont use
        public void getTable()
        {
            Debug.WriteLine("==========================");
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (Blocks[i, j] == null)
                    {
                        Debug.Write("---");
                    }
                    else
                    {
                        Debug.Write(ColorsList[i, j] + "" +
                            ColorsList[i, j] + "" +
                            ColorsList[i, j]);
                    }
                }
                Debug.WriteLine("");

            }
            Debug.WriteLine("==========================");
        }

        //dont use
        public bool[] getNeighbor(int[] coord)
        {
            bool[] Neighbors = { false, false, false }; 
            if (Blocks[coord[0], coord[1] - 1] != null && coord[1] > 0)
            {
                Neighbors[0] = true;
            }
            if (Blocks[coord[0], coord[1] + 1] != null && coord[1] < ColumnCount)
            {
                Neighbors[0] = true;
            }
            if (Blocks[coord[0] + 1, coord[1]] != null && coord[0] < RowCount)
            {
                Neighbors[0] = true;
            }
            Debug.WriteLine("Neighbors: " + Neighbors[0] + "<- " + Neighbors[1] + " ->" + Neighbors[2]);
            return Neighbors;
        }

        
        public int getRandomField()
        {
            return RandomNumber.Next(0, ColumnCount);
        }

        //dont use
        public int[] getCurentBlockCoords()
        {
            return this.CurentCoords;
        }
    }
}
