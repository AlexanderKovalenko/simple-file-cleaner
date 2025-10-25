using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SimpleFileCleaner {
    public partial class Form1 : Form {
        private string[] foldersToSearch = new string[] { "bin", "obj", "node_modules", ".vs", ".angular" };
        private List<string> foundFolders = new List<string>();

        public Form1() {
            InitializeComponent();

            textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void button1_Click(object sender, EventArgs e) {
            using (var fbd = new FolderBrowserDialog()) {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                    textBox1.Text = fbd.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (Directory.Exists(textBox1.Text)) {
                foundFolders.Clear();

                for (int i = 0; i < foldersToSearch.Length; i++) {
                    var currentRes = Directory.EnumerateDirectories(textBox1.Text, foldersToSearch[i], SearchOption.AllDirectories).ToList();

                    for (int j = 0; j < currentRes.Count; j++) {
                        if (!currentRes[j].Contains(@"\node_modules\"))
                            foundFolders.Add(currentRes[j]);
                    }
                }

                textBox2.Text = string.Join("\r\n", foundFolders);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            for (int i = 0; i < foundFolders.Count; i++) {
                Directory.Delete(foundFolders[i], true);
            }
        }
    }
}