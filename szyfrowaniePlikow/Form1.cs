using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace szyfrowaniePlikow
{    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String generujKluczSzyfrowania(String tekst, String klucz)
        {
            int x = tekst.Length;

            for (int i=0;;i++)
            {
                //wroc do pierwszej litery slowa kluczowego
                if (x == i)
                {
                    i = 0;
                }

                //tak dlugo wypelniaj tablice powtorzeniami slowa kluczowego az podmieni caly wprowadzony tekst
                if (klucz.Length == tekst.Length)
                {
                    break;
                }

                //dodaj litere do klucza
                klucz+=(klucz[i]);
            }
            return klucz;
        }

        String zaszyfruj(String tekst, String klucz)
        {
            String zaszyfrowanyTekst = "";

            for (int i = 0; i < tekst.Length; i++)
            {
                //konwersja liter tekstu na numery
                int x = (tekst[i] + klucz[i]) % 26;

                //dodaj do wartosci x wartosc ascii wielkiego a, czyli 65
                x += 'A';

                if (tekst[i]==32)
                {
                    zaszyfrowanyTekst += " ";
                }
                else
                {
                    zaszyfrowanyTekst += (char)(x);
                }
            }
            return zaszyfrowanyTekst;
        }

        String odszyfruj(String zaszyfrowanyTekst, String klucz)
        {
            String oryginal = "";

            for (int i = 0; i < zaszyfrowanyTekst.Length && i < klucz.Length; i++)
            {
                //konwersja liter tekstu na numery
                int x = (zaszyfrowanyTekst[i] - klucz[i] + 26) % 26;

                //dodaj do wartosci x wartosc ascii wielkiego a, czyli 65
                x += 'A';

                if (zaszyfrowanyTekst[i] == 32)
                {
                    oryginal += " ";
                }
                else
                {
                    oryginal += (char)(x);
                }
            }
            return oryginal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (!textBox1.Text.Any(c => char.IsDigit(c)) && !textBox2.Text.Any(c => char.IsDigit(c)))
                    {
                        //zamiana malych liter na wielkie
                        textBox1.Text = textBox1.Text.ToUpper();
                        textBox2.Text = textBox2.Text.ToUpper();

                        //generowanie klucza szyfrowania
                        textBox4.Text = generujKluczSzyfrowania(textBox1.Text, textBox2.Text);

                        //szyfrowanie uzywajac klucza
                        textBox3.Text = zaszyfruj(textBox1.Text, textBox4.Text);
                    }
                    else
                    {
                        MessageBox.Show("Pole z wiadomością i słowo kluczowe nie może zawierać cyfr.");
                    }
                }
                else
                {
                    MessageBox.Show("Pole z wiadomością i/lub słowem kluczowym jest puste.");
                }
            }
            else
            {
                if (textBox3.Text != "" && textBox4.Text != "")
                {
                    //deszyfrowanie uzywajac klucza
                    textBox1.Text=odszyfruj(textBox3.Text, textBox4.Text);
                }
                else
                {
                    MessageBox.Show("Pole z zaszyfrowaną wiadomością i/lub kluczem jest puste.");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = true;
                textBox3.Text = "";
                textBox4.ReadOnly = true;
                textBox4.Text = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                textBox1.ReadOnly = true;
                textBox1.Text = "";
                textBox2.ReadOnly = true;
                textBox2.Text = "";
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
        }
    }
}
