using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckEnd.Model;

namespace CheckEnd
{
    public partial class AnswerControl : UserControl
    {

        public delegate void ReAnswer();

        public string Questions { get; set; }
        public string Answers { get; set; }
        public string Answers2 { get; set; }
        public string Answers3 { get; set; }
        public string Answers4 { get; set; }
        public string ZhengQue { get; set; }
        public Bitmap pic1 { get; set; }
        public Bitmap pic2 { get; set; }
        public Bitmap pic3 { get; set; }
        public Bitmap pic4 { get; set; }
        public string QuestionNumber { get; set; }

        public AnswerControl()
        {
            InitializeComponent();
        }

        public void SetInfo(int width, int height)
        {

            Width = width;
            Height = height / 6;

            question.Location = new Point(width / 30, 0);
            question.Size = new Size(width / 3, height / 6);
            question.Text = Questions;

            answer.Location = new Point((question.Width + width / 6), 0);
            answer.Size = new Size(width / 8, height / 6);
            

            answer2.Location = new Point((answer.Right), 0);
            answer2.Size = new Size(width / 8, height / 6);
          

            answer3.Location = new Point((answer2.Right), 0);
            answer3.Size = new Size(width / 8, height / 6);
           

            answer4.Location = new Point((answer3.Right), 0);
            answer4.Size = new Size(width / 8, height / 6);
           




            panel1.Size = answer.Size;
            panel1.Location = new Point(0, 0);
            panel1.Parent = answer;
            panel1.BackColor = Color.Transparent;

            panel2.Size = answer.Size;
            panel2.Location = new Point(0, 0);
            panel2.Parent = answer2;
            panel2.BackColor = Color.Transparent;

            panel3.Size = answer.Size;
            panel3.Location = new Point(0, 0);
            panel3.Parent = answer3;
            panel3.BackColor = Color.Transparent;

            panel4.Size = answer.Size;
            panel4.Location = new Point(0, 0);
            panel4.Parent = answer4;
            panel4.BackColor = Color.Transparent;

            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();

            answer.Text = "";
            answer2.Text = "";
            answer3.Text = "";
            answer4.Text = "";
            if (pic1 == null)
            {
                answer.Text = Answers;
            }
            if (pic2 == null)
            {
                answer2.Text = Answers2;
            }
            if (pic3 == null)
            {
                answer3.Text = Answers3;
            }
            if (pic4 == null)
            {
                answer4.Text = Answers4;
            }


            //pictureBox1.Paint += picbox_border_Paint;
            //pictureBox2.Paint += picbox_border_Paint;
            //pictureBox3.Paint += picbox_border_Paint;
            //pictureBox4.Paint += picbox_border_Paint;

            answer.Name = Answers;
            answer2.Name = Answers2;
            answer3.Name = Answers3;
            answer4.Name = Answers4;

            answer.BackColor = CancelColor;
            answer2.BackColor = CancelColor;
            answer3.BackColor = CancelColor;
            answer4.BackColor = CancelColor;

            if (pic1 != null)
            {
                answer.Image = pic1; 
            }

            if (pic2 != null)
            {
               answer2.Image = pic2;
                
            }
            if (pic3 != null)
            {
                answer3.Image = pic3;
                
            }
            if (pic4 != null)
            {
                answer4.Image = pic4;
                
            }
            //SetAnswerColor(Questions);

            //指示灯
            //this.judge.Location = new Point(answer4.Right, 0);
            //this.judge.Size = new Size(answer.Width / 2, answer.Height);

            //if(Judge == "true")
            //{
            //    this.judge.BackColor = Color.Green;
            //}
            //else if(Judge == "false")
            //{
            //    this.judge.BackColor = Color.Red;
            //}
            //else
            //{
            //    this.judge.BackColor = Color.Black;
            //}

        } 

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Control con = sender as Control;
            ControlPaint.DrawBorder(e.Graphics, con.ClientRectangle,
            Color.Green, 6, ButtonBorderStyle.Solid, //左边
　　　　　   Color.Green, 6, ButtonBorderStyle.Solid, //上边
　　　　　   Color.Green, 6, ButtonBorderStyle.Solid, //右边
　　　　　   Color.Green, 6, ButtonBorderStyle.Solid);//底边
        }
        //private void picbox_border_Paint(object sender, PaintEventArgs e)
        //{
        //    Control con = sender as Control;
        //    ControlPaint.DrawBorder(e.Graphics, con.ClientRectangle,
        //        Color.Green, 6, ButtonBorderStyle.Solid, //左边
        //        Color.Green, 6, ButtonBorderStyle.Solid, //上边
        //        Color.Green, 6, ButtonBorderStyle.Solid, //右边
        //        Color.Green, 6, ButtonBorderStyle.Solid);//底边
        //}

        private void panel1_Click(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            string name = p.Name;
            switch (name)
            {
                case "panel1":
                    answer_Click(answer, null);
                    break;
                case "panel2":
                    answer_Click(answer2, null);
                    break;
                case "panel3":
                    answer_Click(answer3, null);
                    break;
                case "panel4":
                    answer_Click(answer4, null);
                    break;
            }
        }
        Color ChoiceColor = Color.BlueViolet;
        Color CancelColor = Color.White;
        private void answer_Click(object sender, EventArgs e)
        {
            Label thisLab = sender as Label;
            
            //if(!string.IsNullOrEmpty(thisLab.Text))
            //{
                if (thisLab.BackColor == ChoiceColor)
                {
                    answer.BackColor = CancelColor;
                    answer2.BackColor = CancelColor;
                    answer3.BackColor = CancelColor;
                    answer4.BackColor = CancelColor;

                    answer.Controls[0].Hide();
                    answer2.Controls[0].Hide();
                    answer3.Controls[0].Hide();
                    answer4.Controls[0].Hide();
                }
                else
                {
                    answer.BackColor = CancelColor;
                    answer2.BackColor = CancelColor;
                    answer3.BackColor = CancelColor;
                    answer4.BackColor = CancelColor;

                    answer.Controls[0].Hide();
                    answer2.Controls[0].Hide();
                    answer3.Controls[0].Hide();
                    answer4.Controls[0].Hide();


                    //pictureBox1.Hide();
                    //pictureBox2.Hide();
                    //pictureBox3.Hide();
                    //pictureBox4.Hide();

                    thisLab.BackColor = ChoiceColor;
                    thisLab.Controls[0].Show();
                  
                }
            //
          
        }

        public bool IsSuccess
        {
            get
            {
                bool result = false;
                if (answer.BackColor == ChoiceColor)
                {
                    if (answer.Name == ZhengQue)
                    {
                        return true;
                    }
                }
                if (answer2.BackColor == ChoiceColor)
                {
                    if (answer2.Name == ZhengQue)
                    {
                        return true;
                    }
                }
                if (answer3.BackColor == ChoiceColor)
                {
                    if (answer3.Name == ZhengQue)
                    {
                        return true;
                    }
                }
                if (answer4.BackColor == ChoiceColor)
                {
                    if (answer4.Name == ZhengQue)
                    {
                        return true;
                    }
                }
                return result;
            }
        }
    }
}
