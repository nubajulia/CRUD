namespace ApiCaller
{
    public partial class Form1 : Form
    {
        ConfigReader Config { get; set; }
        public Form1()
        {
            InitializeComponent();

            Config = new ConfigReader();
            label1.Text = Config.test;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new LecturaXML_Nodes().LecturaXML(Config.fichero);
            new LecturaXML_Nodes().LecturaXML_Deserialize(Config.fichero);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new LecturaXML_Nodes().LecturaXML(Config.pizzas);
            new LecturaXML_Nodes().LecturaXML_DeserializePizza(Config.pizzas);
        }
    }
}