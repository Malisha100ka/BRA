using BogRvatiAutomati.Properties;
using PokerOddsCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BogRvatiAutomati
{
    public partial class Form1 : Form
    {
        private int Stage = 0;
        private List<Card> Player1Hand = new List<Card>();
        private List<Card> Player2Hand = new List<Card>();
        private List<Card> Player3Hand = new List<Card>();
        private Boolean Player1 = true;
        private Boolean Player2 = false;
        private Boolean Player3 = false;
        private OddsCalculator calculator = new OddsCalculator();
        private float[] OddsPlayer1 = new float[10];
        private float[] OddsPlayer2 = new float[10];
        private float[] OddsPlayer3 = new float[10];
        private int[] Cards = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private const int SIMS = 1000;
        private const int DEC_PLACES = 2;

        public Form1()
        {
            InitializeComponent();
            SetLabelsNewGame();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            switch (Stage)
            {
                case 0:
                    Stage0();
                    break;
                case 1:
                    Stage1();
                    break;
                case 2:
                    Stage2();
                    break;
                case 3:
                    Stage3();
                    break;
            }
            Stage++;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SetLabelsNewGame();
            ClearPlayers();
            ClearLists();
            ClearOdds();
            ClearCards();
            Stage = 0;
            button1.Enabled = true;
        }

        private void Stage0()
        {
            GenerateCards();
            CountPlayers();
            SetPlayersCard();
            CalculateOdds();
            SetOdds();
            button1.Text = "Flop";
        }

        private void Stage1()
        {
            SetFlopCards();
            CalculateOdds();
            SetOdds();
            button1.Text = "Turn";
        }

        private void Stage2()
        {
            SetTurnCards();
            CalculateOdds();
            SetOdds();
            button1.Text = "River";
        }

        private void Stage3()
        {
            SetRiverCards();
            CalculateOdds();
            SetOdds();
            button1.Text = "--END--";
            button1.Enabled = false;
        }
        
        private void GenerateCards()
        {
            var rand = new Random();
            var number = 0;
            for (int i = 0; i < Cards.Length; i++)
            {
                do
                {
                    number = rand.Next(0, 52);
                } while (Cards.Contains(number));
                Cards[i] = number;
            }
        }
        
        private void CountPlayers()
        {
            if (radioButton1.Checked)
            {
                Player1 = true;
                Player2 = false;
                Player3 = false;
            }
            else if (radioButton2.Checked)
            {
                Player1 = true;
                Player2 = true;
                Player3 = false;
            }
            else
            {
                Player1 = true;
                Player2 = true;
                Player3 = true;
            }
        }

        
        private void SetPlayersCard()
        {
            if (Player1)
            {
                Player1Hand.Add(new Card(Cards[0]));
                Player1Hand.Add(new Card(Cards[1]));
                pictureBox9.Image = getImageByID(Player1Hand[0].CardID);
                pictureBox10.Image = getImageByID(Player1Hand[1].CardID);
            }
            if (Player2)
            {
                Player2Hand.Add(new Card(Cards[2]));
                Player2Hand.Add(new Card(Cards[3]));
                pictureBox2.Image = getImageByID(Player2Hand[0].CardID);
                pictureBox11.Image = getImageByID(Player2Hand[1].CardID);
            }
            if (Player3)
            {
                Player3Hand.Add(new Card(Cards[4]));
                Player3Hand.Add(new Card(Cards[5]));
                pictureBox7.Image = getImageByID(Player3Hand[0].CardID);
                pictureBox8.Image = getImageByID(Player3Hand[1].CardID);
            }
        }


        
       private void SetFlopCards()
       {
           if (Player1)
           {
               Player1Hand.Add(new Card(Cards[6]));
               Player1Hand.Add(new Card(Cards[7]));
               Player1Hand.Add(new Card(Cards[8]));
           }
           if (Player2)
           {
               Player2Hand.Add(new Card(Cards[6]));
               Player2Hand.Add(new Card(Cards[7]));
               Player2Hand.Add(new Card(Cards[8]));
           }
           if (Player3)
           {
               Player3Hand.Add(new Card(Cards[6]));
               Player3Hand.Add(new Card(Cards[7]));
               Player3Hand.Add(new Card(Cards[8]));
           }
           pictureBox1.Image = getImageByID(Player1Hand[2].CardID);
           pictureBox3.Image = getImageByID(Player1Hand[3].CardID);
           pictureBox4.Image = getImageByID(Player1Hand[4].CardID);
       }


       private void SetTurnCards()
       {
           if (Player1)
           {
               Player1Hand.Add(new Card(Cards[9]));
           }
           if (Player2)
           {
               Player2Hand.Add(new Card(Cards[9]));
           }
           if (Player3)
           {
               Player3Hand.Add(new Card(Cards[9]));
           }
           pictureBox5.Image = getImageByID(Player1Hand[5].CardID);
       }


       private void SetRiverCards()
       {
           if (Player1)
           {
               Player1Hand.Add(new Card(Cards[10]));
           }
           if (Player2)
           {
               Player2Hand.Add(new Card(Cards[10]));
           }
           if (Player3)
           {
               Player3Hand.Add(new Card(Cards[10]));
           }
           pictureBox6.Image = getImageByID(Player1Hand[6].CardID);
       }


       private void CalculateOdds()
       {
           if (Player1)
           {
               OddsPlayer1 = calculator.RunSimulations(Player1Hand, SIMS);
           }
           if (Player2)
           {
               OddsPlayer2 = calculator.RunSimulations(Player2Hand, SIMS);
           }
           if (Player3)
           {
               OddsPlayer3 = calculator.RunSimulations(Player3Hand, SIMS);
           }
       }
        

       private void ClearCards()
       {
           for (int i = 0; i < Cards.Length; i++)
           {
               Cards[i] = 0;
           }
       }

       private void ClearOdds()
       {
           for (int i = 0; i < OddsPlayer1.Length; i++)
           {
               OddsPlayer1[i] = 0;
               OddsPlayer2[i] = 0;
               OddsPlayer3[i] = 0;
           }
       }

       private void ClearLists()
       {
           Player1Hand.Clear();
           Player2Hand.Clear();
           Player3Hand.Clear();
       }

       private void ClearPlayers()
       {
           Player1 = true;
           Player2 = false;
           Player3 = false;
       }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SetOdds();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            SetOdds();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            SetOdds();
        }
        private void SetOdds()
       {
           if (radioButton4.Checked)
           {
                label2.Text = Math.Round(OddsPlayer3[0], DEC_PLACES) + "%";
                label4.Text = Math.Round(OddsPlayer3[1], DEC_PLACES) + "%";
                label6.Text = Math.Round(OddsPlayer3[2], DEC_PLACES) + "%";
                label8.Text = Math.Round(OddsPlayer3[3], DEC_PLACES) + "%";
                label10.Text = Math.Round(OddsPlayer3[4], DEC_PLACES) + "%";
                label12.Text = Math.Round(OddsPlayer3[5], DEC_PLACES) + "%";
                label14.Text = Math.Round(OddsPlayer3[6], DEC_PLACES) + "%";
                label16.Text = Math.Round(OddsPlayer3[7], DEC_PLACES) + "%";
                label18.Text = Math.Round(OddsPlayer3[8], DEC_PLACES) + "%";
                label20.Text = Math.Round(OddsPlayer3[9], DEC_PLACES) + "%";
            }
           else if (radioButton5.Checked)
           {
                label2.Text = Math.Round(OddsPlayer1[0], DEC_PLACES) + "%";
                label4.Text = Math.Round(OddsPlayer1[1], DEC_PLACES) + "%";
                label6.Text = Math.Round(OddsPlayer1[2], DEC_PLACES) + "%";
                label8.Text = Math.Round(OddsPlayer1[3], DEC_PLACES) + "%";
                label10.Text = Math.Round(OddsPlayer1[4], DEC_PLACES) + "%";
                label12.Text = Math.Round(OddsPlayer1[5], DEC_PLACES) + "%";
                label14.Text = Math.Round(OddsPlayer1[6], DEC_PLACES) + "%";
                label16.Text = Math.Round(OddsPlayer1[7], DEC_PLACES) + "%";
                label18.Text = Math.Round(OddsPlayer1[8], DEC_PLACES) + "%";
                label20.Text = Math.Round(OddsPlayer1[9], DEC_PLACES) + "%";
            }
           else
           {
                label2.Text = Math.Round(OddsPlayer2[0], DEC_PLACES) + "%";
                label4.Text = Math.Round(OddsPlayer2[1], DEC_PLACES) + "%";
                label6.Text = Math.Round(OddsPlayer2[2], DEC_PLACES) + "%";
                label8.Text = Math.Round(OddsPlayer2[3], DEC_PLACES) + "%";
                label10.Text = Math.Round(OddsPlayer2[4], DEC_PLACES) + "%";
                label12.Text = Math.Round(OddsPlayer2[5], DEC_PLACES) + "%";
                label14.Text = Math.Round(OddsPlayer2[6], DEC_PLACES) + "%";
                label16.Text = Math.Round(OddsPlayer2[7], DEC_PLACES) + "%";
                label18.Text = Math.Round(OddsPlayer2[8], DEC_PLACES) + "%";
                label20.Text = Math.Round(OddsPlayer2[9], DEC_PLACES) + "%";
            }
       }
       
        private void SetLabelsNewGame()
        {
            Image image = Resources.red_back;

            label1.Text = "High Card:";
            label3.Text = "Pair:";
            label5.Text = "Two Pair:";
            label7.Text = "Three of a Kind:";
            label9.Text = "Straight:";
            label11.Text = "Flush:";
            label13.Text = "Full House:";
            label15.Text = "Four of a Kind:";
            label17.Text = "Straight Flush:";
            label19.Text = "Royal Flush:";
            label2.Text = "None";
            label4.Text = "None";
            label6.Text = "None";
            label8.Text = "None";
            label10.Text = "None";
            label12.Text = "None";
            label14.Text = "None";
            label16.Text = "None";
            label18.Text = "None";
            label20.Text = "None";

            pictureBox1.Image = image;
            pictureBox2.Image = image;
            pictureBox3.Image = image;
            pictureBox4.Image = image;
            pictureBox5.Image = image;
            pictureBox6.Image = image;
            pictureBox7.Image = image;
            pictureBox8.Image = image;
            pictureBox9.Image = image;
            pictureBox10.Image = image;
            pictureBox11.Image = image;


            radioButton1.Text = "Jedan igrač";
            radioButton2.Text = "Dva igrača";
            radioButton3.Text = "Tri igrača";
            radioButton4.Text = "Igrač 3";
            radioButton5.Text = "Igrač 1";
            radioButton6.Text = "Igrač 2";

            button1.Text = "Start";
            button2.Text = "RESET -> 0x0000";
        }

        private Image getImageByID(int cardID)
        {
            switch (cardID)
            {
                case 0:
                    return Resources.SA;
                case 1:
                    return Resources.CA;
                case 2:
                    return Resources.HA;
                case 3:
                    return Resources.DA;
                case 4:
                    return Resources.S2;
                case 5:
                    return Resources.C2;
                case 6:
                    return Resources.H2;
                case 7:
                    return Resources.D2;
                case 8:
                    return Resources.S3;
                case 9:
                    return Resources.C3;
                case 10:
                    return Resources.H3;
                case 11:
                    return Resources.D3;
                case 12:
                    return Resources.S4;
                case 13:
                    return Resources.C4;
                case 14:
                    return Resources.H4;
                case 15:
                    return Resources.D4;
                case 16:
                    return Resources.S5;
                case 17:
                    return Resources.C5;
                case 18:
                    return Resources.H5;
                case 19:
                    return Resources.D5;
                case 20:
                    return Resources.S6;
                case 21:
                    return Resources.C6;
                case 22:
                    return Resources.H6;
                case 23:
                    return Resources.D6;
                case 24:
                    return Resources.S7;
                case 25:
                    return Resources.C7;
                case 26:
                    return Resources.H7;
                case 27:
                    return Resources.D7;
                case 28:
                    return Resources.S8;
                case 29:
                    return Resources.C8;
                case 30:
                    return Resources.H8;
                case 31:
                    return Resources.D8;
                case 32:
                    return Resources.S9;
                case 33:
                    return Resources.C9;
                case 34:
                    return Resources.H9;
                case 35:
                    return Resources.D9;
                case 36:
                    return Resources.S10;
                case 37:
                    return Resources.C10;
                case 38:
                    return Resources.H10;
                case 39:
                    return Resources.D10;
                case 40:
                    return Resources.SJ;
                case 41:
                    return Resources.CJ;
                case 42:
                    return Resources.HJ;
                case 43:
                    return Resources.DJ;
                case 44:
                    return Resources.SQ;
                case 45:
                    return Resources.CQ;
                case 46:
                    return Resources.HQ;
                case 47:
                    return Resources.DQ;
                case 48:
                    return Resources.SK;
                case 49:
                    return Resources.CK;
                case 50:
                    return Resources.HK;
                case 51:
                    return Resources.DK;
                default:
                    return Resources.red_back;
            }
        }

    }


}
