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

            this.Width = width;
            this.Height = height / 7;

            this.question.Location = new Point(width / 30, 0);
            this.question.Size = new Size(width / 3, height / 7);
            this.question.Text = Questions;

            this.answer.Location = new Point((question.Width + width / 6), 0);
            this.answer.Size = new Size(width / 8, height / 7);
            this.answer.Text = Answers;

            this.answer2.Location = new Point((answer.Right), 0);
            this.answer2.Size = new Size(width / 8, height / 7);
            this.answer2.Text = Answers2;

            this.answer3.Location = new Point((answer2.Right), 0);
            this.answer3.Size = new Size(width / 8, height / 7);
            this.answer3.Text = Answers3;

            this.answer4.Location = new Point((answer3.Right), 0);
            this.answer4.Size = new Size(width / 8, height / 7);
            this.answer4.Text = Answers4;


            this.panel1.Size = this.answer.Size;
            this.panel1.Location = new Point(0, 0);
            panel1.Parent = answer;
            panel1.BackColor = Color.Transparent;

            this.panel2.Size = this.answer.Size;
            this.panel2.Location = new Point(0, 0);
            panel2.Parent = answer2;
            panel2.BackColor = Color.Transparent;

            this.panel3.Size = this.answer.Size;
            this.panel3.Location = new Point(0, 0);
            panel3.Parent = answer3;
            panel3.BackColor = Color.Transparent;

            this.panel4.Size = this.answer.Size;
            this.panel4.Location = new Point(0, 0);
            panel4.Parent = answer4;
            panel4.BackColor = Color.Transparent;

            this.panel1.Hide();
            this.panel2.Hide();
            this.panel3.Hide();
            this.panel4.Hide();


            this.answer.Name = this.Answers;
            this.answer2.Name = this.Answers2;
            this.answer3.Name = this.Answers3;
            this.answer4.Name = this.Answers4;

            this.answer.BackColor = CancelColor;
            this.answer2.BackColor = CancelColor;
            this.answer3.BackColor = CancelColor;
            this.answer4.BackColor = CancelColor;

            if (pic1 != null)
            {
                this.answer.Image = pic1;
            }
            if (pic2 != null)
            {
                this.answer2.Image = pic2;
            }
            if (pic3 != null)
            {
                this.answer3.Image = pic3;
            }
            if (pic4 != null)
            {
                this.answer4.Image = pic4;
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
            ControlPaint.DrawBorder(e.Graphics, (sender as Panel).ClientRectangle,
            Color.BlueViolet, 6, ButtonBorderStyle.Solid, //左边
　　　　　  Color.BlueViolet, 6, ButtonBorderStyle.Solid, //上边
　　　　　  Color.BlueViolet, 6, ButtonBorderStyle.Solid, //右边
　　　　　  Color.BlueViolet, 6, ButtonBorderStyle.Solid);//底边
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Panel p = sender as Panel;
            string name = p.Name;
            if (name == "panel1")
            {
                answer_Click(this.answer, null);
            }
            if (name == "panel2")
            {
                answer_Click(this.answer2, null);
            }
            if (name == "panel3")
            {
                answer_Click(this.answer3, null);
            }
            if (name == "panel4")
            {
                answer_Click(this.answer4, null);
            }
        }
        Color ChoiceColor = Color.BlueViolet;
        Color CancelColor = Color.White;
        private void answer_Click(object sender, EventArgs e)
        {
            Label thisLab = sender as Label;
            
            if(!string.IsNullOrEmpty(thisLab.Text))
            {
                if (thisLab.BackColor == ChoiceColor)
                {
                    this.answer.BackColor = CancelColor;
                    this.answer2.BackColor = CancelColor;
                    this.answer3.BackColor = CancelColor;
                    this.answer4.BackColor = CancelColor;

                    this.answer.Controls[0].Hide();
                    this.answer2.Controls[0].Hide();
                    this.answer3.Controls[0].Hide();
                    this.answer4.Controls[0].Hide();
                }
                else
                {
                    this.answer.BackColor = CancelColor;
                    this.answer2.BackColor = CancelColor;
                    this.answer3.BackColor = CancelColor;
                    this.answer4.BackColor = CancelColor;

                    this.answer.Controls[0].Hide();
                    this.answer2.Controls[0].Hide();
                    this.answer3.Controls[0].Hide();
                    this.answer4.Controls[0].Hide();
                    thisLab.BackColor = ChoiceColor;
                    thisLab.Controls[0].Show();
                }
            }
          
        }

        public bool IsSuccess
        {
            get
            {
                bool result = false;
                if (this.answer.BackColor == ChoiceColor)
                {
                    if (this.answer.Name == this.ZhengQue)
                    {
                        return true;
                    }
                }
                if (this.answer2.BackColor == ChoiceColor)
                {
                    if (this.answer2.Name == this.ZhengQue)
                    {
                        return true;
                    }
                }
                if (this.answer3.BackColor == ChoiceColor)
                {
                    if (this.answer3.Name == this.ZhengQue)
                    {
                        return true;
                    }
                }
                if (this.answer4.BackColor == ChoiceColor)
                {
                    if (this.answer4.Name == this.ZhengQue)
                    {
                        return true;
                    }
                }
                return result;
            }
        }
    }
}
