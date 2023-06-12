using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;

namespace LR27_Sydorenko_202TN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Показати список дисків";
            button2.Text = "Переміщення по файловій смстемі";
            button3.Text = "Перегляд основних властивостей виділеного диску";
            button4.Text = "перегляд основних властивостей виділеного каталогу";
            button5.Text = "перегляд основних властивостей виділеного файлу";
            button6.Text = "фільтрацію списку файлів";
            button7.Text = "перегляд вмісту графічних файлів";
            button8.Text = "перегляд атрибутів безпеки файлів та каталогів";
            button9.Text = "перегляд вмісту текстових файлів";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DriveInfo[] Drivers = DriveInfo.GetDrives();
            foreach (DriveInfo drive in Drivers)
            {
                treeView1.Nodes.Add(drive.Name);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\Lenovo\OneDrive\Рабочий стол";
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string file in files)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(file));
                item.SubItems.Add(Path.GetExtension(file));
                item.SubItems.Add(File.GetLastWriteTime(file).ToString());
                listView1.Items.Add(item);
            }
            foreach (string dir in dirs)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(dir));
                item.SubItems.Add("Folder");
                item.SubItems.Add(Directory.GetLastWriteTime(dir).ToString());
                listView1.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DriveInfo drive = new DriveInfo(textBox1.Text);
            label4.Text = Convert.ToString(drive.TotalSize);
            label5.Text = Convert.ToString(drive.TotalFreeSpace);
            //C
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(textBox2.Text);
            label7.Text = Convert.ToString(directory.CreationTime);
            label6.Text = Convert.ToString(directory.GetFiles().Length);
            //C:\Windows
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(textBox3.Text);
            label12.Text = Convert.ToString(file.Length);
            label11.Text = Convert.ToString(file.CreationTime);
            //C:\Users\Lenovo\OneDrive\Рабочий стол\English.txt
        }

        private void button6_Click(object sender, EventArgs e)
        {           
            DirectoryInfo directory = new DirectoryInfo(textBox4.Text);
            FileSystemInfo[] filesAndDirs = directory.GetFileSystemInfos(); // получаем массив файлов и каталогов
            string result = "";
            foreach (FileSystemInfo fileOrDir in filesAndDirs)
            {
                if (fileOrDir.Attributes.HasFlag(FileAttributes.Directory)) 
                {
                    result += "[" + fileOrDir.Name + "], "; 
                }
                else if (fileOrDir.Extension == ".txt") 
                {
                    result += fileOrDir.Name + ", "; 
                }
            }
            label17.Text = result.TrimEnd(',', ' '); 
            //C:\Windows

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(textBox5.Text);
            if (extension.Contains(".jpg") || extension.Contains(".png") || extension.Contains(".bmp") || extension.Contains(".gif"))
            {
                Image image = Image.FromFile(textBox5.Text);
                pictureBox1.Image = image;
            }
            //D:\Skren\151878457419472161.jpg
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string filePath = textBox6.Text;
            if (File.Exists(filePath))
            {
                FileSecurity fileSecurity = File.GetAccessControl(filePath);
                string securityDescriptor = fileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
                label20.Text = securityDescriptor;
            }
            else if (Directory.Exists(filePath))
            {
                DirectorySecurity directorySecurity = Directory.GetAccessControl(filePath);
                string securityDescriptor = directorySecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
                label20.Text = securityDescriptor;
                //D:\Skren\151878457419472161.jpg
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {            
            string deg = File.ReadAllText(textBox7.Text);
            textBox8.Text = deg;
            //C:\Users\Lenovo\OneDrive\Рабочий стол\Прект курсової Сидоренко  202ТН.txt
        }
    }
}
