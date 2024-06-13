using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;



namespace DS1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Try a Number", "Invalid Data type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Try a Number", "Invalid Data type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (!char.IsControl(e.KeyChar) && (!char.IsWhiteSpace(e.KeyChar))))
            {
                MessageBox.Show("Try a letter", "Invalid Data type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (!char.IsControl(e.KeyChar) && (!char.IsWhiteSpace(e.KeyChar))))
            {
                MessageBox.Show("Try a letter", "Invalid Data type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Date d'entrée ne peut pas etre plus grand que " + DateTime.Now, "Invalid Data Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Fichier texte|*.txt";
            saveFileDialog1.Title = "Enregistrer les données dans un fichier texte";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                try
                {

                    using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName, true))
                    {
                        writer.WriteLine(maskedTextBox1.Text + ";" + textBox2.Text + ";" + textBox3.Text + ";" + maskedTextBox2.Text + ";" + dateTimePicker1.Value);
                        writer.Flush();
                    }

                    MessageBox.Show("Les données ont été enregistrées avec succès.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite lors de l'enregistrement du fichier : " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Fichier texte|*.txt";
            openFileDialog1.Title = "Charger les données des patients depuis un fichier texte";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                try
                {
                   
                    using (StreamReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        
                        string content = reader.ReadToEnd();
                        string[] lines = content.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string line in lines)
                        {
                            string[] values = line.Split(';');
                            listView1.Items.Add(new ListViewItem(values));
                        }
                    }

                    MessageBox.Show("Les données ont été chargées avec succès.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite lors du chargement du fichier : " + ex.Message);
                }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Fichier texte|*.txt";
                openFileDialog1.Title = "Rechercher une donnée dans un fichier texte";
                openFileDialog1.ShowDialog();

                if (openFileDialog1.FileName != "")
                {
                    string searchWord = textBox1.Text.Trim();
                    string fileContent = File.ReadAllText(openFileDialog1.FileName);
                    MatchCollection matches = Regex.Matches(fileContent, searchWord);
                    if (matches.Count > 0)
                    {
                        richTextBox1.Clear();
                        foreach (Match match in matches)
                        {
                            richTextBox1.AppendText(match.Value + Environment.NewLine);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aucun résultat trouvé.");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Une erreur s'est produite lors de la recherche.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

