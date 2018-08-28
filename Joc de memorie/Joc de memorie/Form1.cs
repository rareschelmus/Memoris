using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Joc_de_memorie
{
    public partial class Form1 : Form
    {
        int nivel = 5, patrate_corecte = 0, greseli = 3, timp, scor = 0,scor_maxim;
        List<Point> puncte = new List<Point>();
        int allowClick;
        Random poz = new Random();

        public Form1()
        {
            InitializeComponent();
            init_scor_maxim();
        }

        private void init_scor_maxim()
        {
            StreamReader f = new StreamReader("scor.txt");
            scor_maxim = Convert.ToInt32(f.ReadLine());
            label8.Text = Convert.ToString(scor_maxim);
            f.Close();
        }

        private void scrie_scor_maxim()
        {
            StreamWriter g = new StreamWriter("scor.txt");
            g.Write(Convert.ToString(scor_maxim));
            label8.Text = Convert.ToString(scor_maxim);
            g.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            if (allowClick == 0) return;
            var pic = (PictureBox)sender;
            if (Convert.ToInt16(pic.Tag) == 1)
            {
                pic.Image = Properties.Resources._2;
                patrate_corecte++;
                scor++;
                label6.Text = Convert.ToString(scor);
                if (patrate_corecte == nivel)
                {
                    allowClick = 0;
                    scor += timp;
                    label6.Text = Convert.ToString(scor);
                    timer2.Stop();
                    if (scor > scor_maxim)
                    {
                        scor_maxim = scor;
                        scrie_scor_maxim();
                    }
                    if (nivel > 18)
                    {
                        nivel = 5;
                        MessageBox.Show("Ai Castigat!");
                    }
                    else
                    {
                        nivel++;
                        MessageBox.Show("Nivelul urmator");
                    }
                }
            }
            else if (Convert.ToInt16(pic.Tag) == 0)
            {
                pic.Image = Properties.Resources._3;
                pic.Tag = 2;
                greseli--;
                if(scor>0) 
                    scor--;
                label6.Text = Convert.ToString(scor);
                if (greseli == 0)
                {
                    timer2.Stop();
                    allowClick = 0;
                    if (scor >= nivel) scor -= nivel;
                    if (nivel > 5) nivel--;
                    MessageBox.Show("Ai pierdut.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = "15";
            int i = 1, next;
            greseli = 3;
            patrate_corecte = 0;
            Point p = new Point();
            allowClick = 0;
            label4.Text = Convert.ToString(nivel-4);
            label6.Text = Convert.ToString(scor);
            foreach (PictureBox pic in panel1.Controls)
            {
                pic.Image = Properties.Resources._1;
                pic.Tag = 0;
            }
            foreach (PictureBox pic in panel1.Controls)
            {
                if (i <= nivel)
                {
                    pic.Tag = 1;
                    pic.Image = Properties.Resources._2;
                    i++;
                }
                puncte.Add(pic.Location);
            }
            foreach (PictureBox pic in panel1.Controls)
            {
                next = poz.Next(puncte.Count);
                p = puncte[next];
                pic.Location = p;
                puncte.Remove(p);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Apasati pe butonul <Start> pentru a incepe jocul. Timp de 3 secunde trebuie sa memorati patratele colorate cu albastru, dupa scurgerea celor 3 secunde, toate patratele vor reveni la culoarea maro si va trebuii sa apasati pe patratele care au fost albastre intr-un timp maxim de 15 secunde. Sunt permise 3 greseli.", "Instructiuni");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("    Soft creat de Rareș Chelmuș \n                       2015", "Credits");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            foreach (PictureBox pic in panel1.Controls)
            {
                pic.Image = Properties.Resources._1;
            }
            allowClick = 1;
            timer2.Start();
            timp = 15;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(timp);
            timp = timp - 1;
            if (timp == 0)
            {
                timer2.Stop();
                allowClick = 0;
                MessageBox.Show("A expirat timpul");
                if (nivel > 5) nivel--;
                if (scor >= nivel) scor -= nivel;
                label6.Text = Convert.ToString(scor);
            }
        }       
    }
}
