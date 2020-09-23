using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProvaProgramação
{
    public partial class Form1 : Form
    {
        string diretorio = $@"{AppDomain.CurrentDomain.BaseDirectory}\ArquivoFilmes.json";
        Point DragCursor;
        Point DragForm;
        bool Dragging;

        List<Filmes> listaDeFilmes = new List<Filmes>();
        public Form1()
        {
            InitializeComponent();
            this.ActiveControl = textBoxNome;
            LerJson();
        }
        private void LerJson()
        {
            try
            {
                Arquivo.Ler(ref listaDeFilmes, diretorio);
                MostrarFilmes();
            }
            catch (Exception erro) { MessageBox.Show("Erro inesperado!!"); }
        }
        //ADICIONAR UM NOVO FILME;
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBoxNome.Text) || maskedTextBoxNota.Text == " ,")
            {
                MessageBox.Show("preencha todos os campos");
                return;
            }
            listaDeFilmes.Add(new Filmes(textBoxNome.Text, float.Parse(maskedTextBoxNota.Text), dateTimePickerData.Value));
            MostrarFilmes();
            LimparCamposEdicao();
            textBoxNome.Focus();
        }
        //LIMPAR CAMPOS DE ADIÇÃO;
        private void LimparCamposAdicao()
        {
            textBoxNome.Clear();
            maskedTextBoxNota.Clear();
            dateTimePickerData.ResetText();
        }
        //EDITAR FILMES;
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNome2.Text) || maskedTextBoxNota2.Text == " ,")
            {
                MessageBox.Show("preencha todos os campos");
                return;
            }
            int index = int.Parse(textBoxMostrar.Text);
            listaDeFilmes[index].AlterarFilme(textBoxNome2.Text, float.Parse(maskedTextBoxNota2.Text), dateTimePickerData2.Value);
            MostrarFilmes();
            LimparCamposEdicao();
            textBoxMostrar.Focus();
            label9.Visible = false;
        }
        //ECLUIR FILME;
        private void button3_Click(object sender, EventArgs e)
        {
            int index = int.Parse(textBoxMostrar.Text);
            if(MessageBox.Show("Deseja mesmo excluir esse filme?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            listaDeFilmes.RemoveAt(index);
            MostrarFilmes();
            LimparCamposEdicao();
            textBoxMostrar.Focus();
            label9.Visible = false;
        }
        //LIMPAR CAMPOS DE EDIÇÃO;
        private void LimparCamposEdicao()
        {
            textBoxNome2.Clear();
            maskedTextBoxNota2.Clear();
            dateTimePickerData2.ResetText();
            textBoxMostrar.Clear();
        }
        //MOSTRAR OS FILMES NO LISTBOX;
        public void MostrarFilmes()
        {
            listView1.Items.Clear();
            foreach (Filmes f in listaDeFilmes)
            {
                string[] item = new string[4] { listaDeFilmes.IndexOf(f).ToString(), f.NomeDoFilme, f.NotaDoFilme.ToString(), f.DataDeLancamento.ToString("dd / MM / yyyy") };
                
                listView1.Items.Add(new ListViewItem(item));
            }
            label4.Text = $"Quantidade de filmes: {listaDeFilmes.Count}";
        }
        //MOSTRAR DADOS DE UM FILME NOS CAMPOS DE EDIÇÃO A PARTIR DO ID;
        private void textBoxMostrar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int index = int.Parse(textBoxMostrar.Text);
                if (index > listaDeFilmes.Count - 1 || textBoxMostrar.Text == "")
                {
                    LimparCamposEdicao();
                    HabilitarCampos(false);
                    label9.Visible = true;
                    return;
                }
                label9.Visible = false;
                textBoxNome2.Text = listaDeFilmes[index].NomeDoFilme;
                maskedTextBoxNota2.Text = listaDeFilmes[index].NotaDoFilme.ToString();
                dateTimePickerData2.Value = listaDeFilmes[index].DataDeLancamento;
                HabilitarCampos(true);
                
            }
            catch (Exception erro)
            {
                LimparCamposEdicao();
                HabilitarCampos(false);
                label9.Visible = true;
                return;
            }
            
        }
        //HABILITAR/DESABILITAR CAMPOS;
        private void HabilitarCampos(bool a)
        {
            textBoxNome2.Enabled = a;
            maskedTextBoxNota2.Enabled = a;
            dateTimePickerData2.Enabled = a;
            button3.Enabled = a;
            button2.Enabled = a;
        }
        //MOSTRAR DADOS DE UM FILME NOS CAMPOS DE EDIÇÃO A PARTIR DO CLICK;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectTab(1);
                textBoxMostrar.Text = listView1.SelectedIndices[0].ToString();
            }
            catch (Exception erro) { }
        }
        //MUDAR DE GUIA;
        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }
        //FECHAR E SALVAR;
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Arquivo.Salvar(listaDeFilmes, diretorio);
            }
            catch (Exception erro) { MessageBox.Show("Erro inesperado!!"); }

            this.Close();
        }
        //MOVER FORMULÁRIO COM PAINEL
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }
    }
}
