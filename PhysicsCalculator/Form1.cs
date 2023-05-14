using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PhysicsCalculator
{
    public partial class Form1 : Form
    {
        // Default src location
        string picPath = Path.GetDirectoryName(Application.StartupPath) + "//img//";

        public Form1()
        {
            InitializeComponent();
        }

        // Form

        private void Form1_Load(object sender, EventArgs e)
        {
            // Default menu
            comboBox0.SelectedIndex = 0; // SUVAT

            // SUVAT Default units (SI)
            comboBox1.SelectedIndex = 0; // m
            comboBox2.SelectedIndex = 0; // m/s
            comboBox3.SelectedIndex = 0; // m/s
            comboBox4.SelectedIndex = 0; // m/s^2
            comboBox5.SelectedIndex = 0; // s

            // SUVAT Image
            pictureBox1.ImageLocation = picPath + "pic1.png";
            pictureBox2.ImageLocation = picPath + "pic2.png";
            pictureBox3.ImageLocation = picPath + "pic3.jpg";

            // PROJECTILE Default units (SI)
            comboBox6.SelectedIndex = 0; // m/s
            comboBox7.SelectedIndex = 0; // rad
            comboBox8.SelectedIndex = 0; // m
            comboBox9.SelectedIndex = 0; // m
            comboBox10.SelectedIndex = 0; // m/s
            comboBox11.SelectedIndex = 0; // m/s
            comboBox12.SelectedIndex = 0; // m
            comboBox13.SelectedIndex = 0; // s
            comboBox14.SelectedIndex = 0; // m
        }

        // Drop down

        private void comboBox0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox0.SelectedIndex == 0) // SUVAT
            {
                suvat.Visible = true;
                projectile.Visible = false;
                suvatExample.Visible = false;
                projectileExample.Visible = false;
            }
            else if (comboBox0.SelectedIndex == 1) // PROJECTILE
            {
                suvat.Visible = false;
                projectile.Visible = true;
                suvatExample.Visible = false;
                projectileExample.Visible = false;
            }
        }

        // Example Button

        private void button0_Click(object sender, EventArgs e)
        {
            if (comboBox0.SelectedIndex == 0) // SUVAT EXAMPLE
            {
                if (suvatExample.Visible == false)
                {
                    suvatExample.Visible = true;
                }
                else
                {
                    suvatExample.Visible = false;
                }
            }
            else if (comboBox0.SelectedIndex == 1) // PROJECTILE EXAMPLE
            {
                if (projectileExample.Visible == false)
                {
                    projectileExample.Visible = true;
                }
                else
                {
                    projectileExample.Visible = false;
                }
            }
        }

        // SUVAT

        private void button1_Click(object sender, EventArgs e)
        {
            double s = 0, u = 0, u2 = 0, v = 0, v2 = 0, a = 0, t = 0, t2 = 0;
            bool validS = false, validU = false, validU2 = false, validV = false, validV2 = false, validA = false, validT = false, validT2 = false;
            string textS = textBox1.Text, textU = textBox2.Text, textV = textBox3.Text, textA = textBox4.Text, textT = textBox5.Text;

            if (button1.Text == "Find")
            {
                // Changing button state
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;

                button1.Text = "Reset";
                button1.BackColor = Color.Yellow;

                // Getting value and Converting units
                if (Double.TryParse(textS, out s))
                {
                    validS = true;

                    if (comboBox1.SelectedIndex == 1) // km
                    {
                        s *= 1000;
                    }
                }
                if (Double.TryParse(textU, out u))
                {
                    validU = true;

                    if (comboBox2.SelectedIndex == 1) // m/min
                    {
                        u *= 1 / 60;
                    }
                    else if (comboBox2.SelectedIndex == 2) // m/hr
                    {
                        u *= 1 / 3600;
                    }
                    else if (comboBox2.SelectedIndex == 3) // km/s
                    {
                        u *= 1000;
                    }
                    else if (comboBox2.SelectedIndex == 4) // km/min
                    {
                        u *= 1000 / 60;
                    }
                    else if (comboBox2.SelectedIndex == 5) // km/hr
                    {
                        u *= 1000 / 3600;
                    }
                }
                if (Double.TryParse(textV, out v))
                {
                    validV = true;

                    if (comboBox3.SelectedIndex == 1) // m/min
                    {
                        v *= 1 / 60;
                    }
                    else if (comboBox3.SelectedIndex == 2) // m/hr
                    {
                        v *= 1 / 3600;
                    }
                    else if (comboBox3.SelectedIndex == 3) // km/s
                    {
                        v *= 1000;
                    }
                    else if (comboBox3.SelectedIndex == 4)// km/min
                    {
                        v *= 1000 / 60;
                    }
                    else if (comboBox3.SelectedIndex == 5) // km/hr
                    {
                        v *= 1000 / 3600;
                    }
                }
                if (Double.TryParse(textA, out a))
                {
                    validA = true;
                }
                if (Double.TryParse(textT, out t))
                {
                    validT = true;

                    if (comboBox5.SelectedIndex == 1) // min
                    {
                        t *= 60;
                    }
                    if (comboBox5.SelectedIndex == 2) // hr
                    {
                        t *= 3600;
                    }
                }

                // Calculating suvat
                if (validS && validU && validV)
                {
                    validA = false;
                    validT = false;

                    if (u + v != 0)
                    {
                        t = (2 * s) / (u + v);
                        if (t >= 0)
                        {
                            validT = true;
                        }
                    }
                    if (s != 0)
                    {
                        a = (v * v - u * u) / (2 * s);
                        validA = true;
                    }

                    label8.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                }
                else if (validS && validU && validA)
                {
                    validV = false;
                    validT = false;

                    if (u * u + 2 * a * s >= 0)
                    {
                        v = Math.Sqrt(u * u + 2 * a * s);
                        v2 = -v;
                        validV = true;
                        validV2 = true;
                    }
                    if (a != 0 && validV)
                    {
                        t = (v - u) / a;
                        t2 = (v2 - u) / a;

                        if (t >= 0 || t2 >= 0)
                        {
                            validT = true;
                            validT2 = true;
                        }
                    }

                    label7.ForeColor = Color.Red;
                    label8.ForeColor = Color.Red;
                }
                else if (validS && validU && validT)
                {
                    validV = false;
                    validA = false;

                    if (t > 0)
                    {
                        v = (2 * s - u * t) / t;
                        a = (v - u) / t;
                        validV = true;
                        validA = true;
                    }

                    label7.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                }
                else if (validS && validV && validA)
                {
                    validU = false;
                    validT = false;

                    if (v * v - 2 * a * s >= 0)
                    {
                        u = Math.Sqrt(v * v - 2 * a * s);
                        u2 = -u;
                        validU = true;
                        validU2 = true;
                    }
                    if (a != 0 && validU)
                    {
                        t = (v - u) / a;
                        t2 = (v - u2) / a;

                        if (t >= 0 || t2 >= 0)
                        {
                            validT = true;
                            validT2 = true;
                        }
                    }

                    label7.ForeColor = Color.Red;
                    label8.ForeColor = Color.Red;
                }
                else if (validS && validV && validT)
                {
                    validU = false;
                    validA = false;

                    if (t > 0)
                    {
                        u = (2 * s - v * t) / t;
                        a = (v - u) / t;
                        validU = true;
                        validA = true;
                    }

                    label7.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                }
                else if (validS && validA && validT)
                {
                    validU = false;
                    validV = false;

                    if (t > 0)
                    {
                        u = (2 * s - a * t * t) / (2 * t);
                        v = (2 * s + a * t * t) / (2 * t);
                        validU = true;
                        validV = true;
                    }

                    label10.ForeColor = Color.Red;
                    label11.ForeColor = Color.Red;
                }
                else if (validU && validV && validA)
                {
                    validS = false;
                    validT = false;

                    if (a != 0)
                    {
                        t = (v - u) / a;
                        if (t >= 0)
                        {
                            s = (u + v) * t / 2;
                            validT = true;
                            validS = true;
                        }
                    }

                    label7.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                }
                else if (validU && validV && validT)
                {
                    validS = false;
                    validA = false;

                    if (t > 0)
                    {
                        s = (u + v) * t / 2;
                        a = (v - u) / t;
                        validS = true;
                        validA = true;
                    }

                    label7.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                }
                else if (validU && validA && validT)
                {
                    validS = false;
                    validV = false;

                    if (t >= 0)
                    {
                        v = u + (a * t);
                        s = (u * t) + (a * t * t) / 2;
                        validS = true;
                        validV = true;
                    }

                    label7.ForeColor = Color.Red;
                    label10.ForeColor = Color.Red;
                }
                else if (validV && validA && validT)
                {
                    validS = false;
                    validU = false;

                    if (t >= 0)
                    {
                        u = v - (a * t);
                        s = (v * t) - (a * t * t) / 2;
                        validS = true;
                        validU = true;
                    }

                    label7.ForeColor = Color.Red;
                    label11.ForeColor = Color.Red;
                }

                // Converting SI to units
                if (comboBox1.SelectedIndex == 1) // km
                {
                    s /= 1000;
                }

                if (comboBox2.SelectedIndex == 1) // m/min
                {
                    u *= 60;
                    u2 *= 60;
                }
                else if (comboBox2.SelectedIndex == 2) // m/hr
                {
                    u *= 3600;
                    u2 *= 3600;
                }
                else if (comboBox2.SelectedIndex == 3) // km/s
                {
                    u /= 1000;
                    u2 /= 1000;
                }
                else if (comboBox2.SelectedIndex == 4) // km/min
                {
                    u *= 60 / 1000;
                    u2 *= 60 / 1000;
                }
                else if (comboBox2.SelectedIndex == 5) // km/hr
                {
                    u *= 3600 / 1000;
                    u2 *= 3600 / 1000;
                }

                if (comboBox3.SelectedIndex == 1) // m/min
                {
                    v *= 60;
                    v2 *= 60;
                }
                else if (comboBox3.SelectedIndex == 2) // m/hr
                {
                    v *= 3600;
                    v2 *= 3600;
                }
                else if (comboBox3.SelectedIndex == 3) // km/s
                {
                    v /= 1000;
                    v2 /= 1000;
                }
                else if (comboBox3.SelectedIndex == 4) // km/min
                {
                    v *= 60 / 1000;
                    v2 *= 60 / 1000;
                }
                else if (comboBox3.SelectedIndex == 5) // km/hr
                {
                    v *= 3600 / 1000;
                    v2 *= 3600 / 1000;
                }

                if (comboBox5.SelectedIndex == 1) // min
                {
                    t /= 60;
                }
                else if (comboBox5.SelectedIndex == 2) // hr
                {
                    t /= 3600;
                }

                // Showing suvat
                textBox1.Text = "-";
                textBox2.Text = "-";
                textBox3.Text = "-";
                textBox4.Text = "-";
                textBox5.Text = "-";

                if (validS)
                {
                    textBox1.Text = s.ToString("N3");
                }
                if (validU)
                {
                    textBox2.Text = u.ToString("N3");

                    if (validU2)
                    {
                        textBox2.Text += ", " + u2.ToString("N3");
                    }
                }
                if (validV)
                {
                    textBox3.Text = v.ToString("N3");

                    if (validV2)
                    {
                        textBox3.Text += ", " + v2.ToString("N3");
                    }
                }
                if (validA)
                {
                    textBox4.Text = a.ToString("N3");
                }
                if (validT)
                {
                    textBox5.Text = t.ToString("N3");

                    if (validT2)
                    {
                        textBox5.Text += ", " + t2.ToString("N3");
                    }
                }
            }
            else
            {
                // Changing button state
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;

                button1.Text = "Find";
                button1.BackColor = Color.PaleGreen;

                // Reset color
                label7.ForeColor = SystemColors.WindowText;
                label8.ForeColor = SystemColors.WindowText;
                label9.ForeColor = SystemColors.WindowText;
                label10.ForeColor = SystemColors.WindowText;
                label11.ForeColor = SystemColors.WindowText;

                // Clearing value in textbox
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                // Default units (SI)
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
            }
        }

        // PROJECTILE

        private void button2_Click(object sender, EventArgs e)
        {
            double u = 0, ux = 0, uy = 0, theta = 0, sx = 0, sy = 0, h1 = 0, h2 = 0, hmax = 0, ay = 0, g = 9.80665, t = 0;
            bool validU = false, validUx = false, validUy = false, validTheta = false, validH1 = false, validH2 = false, validSx = false, validT = false, validHmax = false;
            string textU = textBox6.Text, textTheta = textBox7.Text, textH1 = textBox8.Text, textH2 = textBox9.Text;

            if (button2.Text == "Find")
            {
                // Changing button state
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;

                button2.Text = "Reset";
                button2.BackColor = Color.Yellow;

                // Getting value and Converting units
                if (Double.TryParse(textU, out u))
                {
                    validU = true;

                    if (u < 0)
                    {
                        textBox6.ForeColor = Color.Red;
                    }

                    if (comboBox6.SelectedIndex == 1) // m/min
                    {
                        u *= 1 / 60;
                    }
                    else if (comboBox6.SelectedIndex == 2) // m/hr
                    {
                        u *= 1 / 3600;
                    }
                    else if (comboBox6.SelectedIndex == 3) // km/s
                    {
                        u *= 1000;
                    }
                    else if (comboBox6.SelectedIndex == 4) // km/min
                    {
                        u *= 1000 / 60;
                    }
                    else if (comboBox6.SelectedIndex == 5) // km/hr
                    {
                        u *= 1000 / 3600;
                    }
                }
                if (Double.TryParse(textTheta, out theta))
                {
                    validTheta = true;

                    if (comboBox7.SelectedIndex == 1) // deg
                    {
                        theta *= Math.PI / 180;
                    }
                }
                if (Double.TryParse(textH1, out h1))
                {
                    validH1 = true;

                    if (comboBox8.SelectedIndex == 1) // km
                    {
                        h1 *= 1000;
                    }
                }
                if (Double.TryParse(textH2, out h2))
                {
                    validH2 = true;

                    if (comboBox9.SelectedIndex == 1) // km
                    {
                        h2 *= 1000;
                    }
                }

                // Calculating PROJECTILE
                if (validU && validTheta && validH1 && validH2)
                {
                    if (u >= 0 && h1 >= 0 && h2 >= 0 && theta >= -Math.PI / 2 && theta <= Math.PI / 2)
                    {
                        ux = u * Math.Cos(theta);
                        uy = u * Math.Sin(theta);
                        u = Math.Sqrt(ux * ux + uy * uy);

                        validUx = true;
                        validUy = true;

                        if (uy >= 0)
                        {
                            ay = -g;
                            sy = -Math.Abs(h1 - h2);
                        }
                        else
                        {
                            ay = g;
                            sy = Math.Abs(h1 - h2);
                        }

                        if (uy * uy + 2 * ay * sy >= 0)
                        {
                            t = (-uy - Math.Sqrt(uy * uy + 2 * ay * sy)) / ay;

                            if (t >= 0)
                            {
                                sx = ux * t;
                                hmax = Math.Min(h1, h2) - (uy * uy) / (2 * ay);

                                validSx = true;
                                validHmax = true;
                                validT = true;
                            }
                        }
                    }
                }

                // Converting SI to units
                if (comboBox6.SelectedIndex == 1) // m/min
                {
                    u *= 60;
                }
                else if (comboBox6.SelectedIndex == 2) // m/hr
                {
                    u *= 3600;
                }
                else if (comboBox6.SelectedIndex == 3) // km/s
                {
                    u /= 1000;
                }
                else if (comboBox6.SelectedIndex == 4) // km/min
                {
                    u *= 60 / 1000;
                }
                else if (comboBox6.SelectedIndex == 5) // km/hr
                {
                    u *= 3600 / 1000;
                }

                if (comboBox7.SelectedIndex == 1) // deg
                {
                    theta *= 180 / Math.PI;
                }

                if (comboBox8.SelectedIndex == 1) // km
                {
                    h1 /= 1000;
                }

                if (comboBox9.SelectedIndex == 1) // km
                {
                    h1 /= 1000;
                }

                if (comboBox10.SelectedIndex == 1) // m/min
                {
                    ux *= 60;
                }
                else if (comboBox10.SelectedIndex == 2) // m/hr
                {
                    ux *= 3600;
                }
                else if (comboBox10.SelectedIndex == 3) // km/s
                {
                    ux /= 1000;
                }
                else if (comboBox10.SelectedIndex == 4) // km/min
                {
                    ux *= 60 / 1000;
                }
                else if (comboBox10.SelectedIndex == 5) // km/hr
                {
                    ux *= 3600 / 1000;
                }

                if (comboBox11.SelectedIndex == 1) // m/min
                {
                    uy *= 60;
                }
                else if (comboBox11.SelectedIndex == 2) // m/hr
                {
                    uy *= 3600;
                }
                else if (comboBox11.SelectedIndex == 3) // km/s
                {
                    uy /= 1000;
                }
                else if (comboBox11.SelectedIndex == 4)// km/min
                {
                    uy *= 60 / 1000;
                }
                else if (comboBox11.SelectedIndex == 5) // km/hr
                {
                    uy *= 3600 / 1000;
                }

                if (comboBox12.SelectedIndex == 1) // km
                {
                    sx /= 1000;
                }

                if (comboBox13.SelectedIndex == 1) // min
                {
                    t /= 60;
                }
                else if (comboBox13.SelectedIndex == 2) // hr
                {
                    t /= 3600;
                }

                if (comboBox14.SelectedIndex == 1) // km
                {
                    hmax /= 1000;
                }

                // Showing PROJECTILE
                textBox6.Text = "-";
                textBox7.Text = "-";
                textBox8.Text = "-";
                textBox9.Text = "-";
                textBox10.Text = "-";
                textBox11.Text = "-";
                textBox12.Text = "-";
                textBox13.Text = "-";
                textBox14.Text = "-";

                if (validU)
                {
                    textBox6.Text = u.ToString("N3");
                }
                if (validTheta)
                {
                    textBox7.Text = theta.ToString("N3");
                }
                if (validH1)
                {
                    textBox8.Text = h1.ToString("N3");
                }
                if (validH2)
                {
                    textBox9.Text = h2.ToString("N3");
                }
                if (validUx)
                {
                    textBox10.Text = ux.ToString("N3");
                }
                if (validUy)
                {
                    textBox11.Text = uy.ToString("N3");
                }
                if (validSx)
                {
                    textBox12.Text = sx.ToString("N3");
                }
                if (validT)
                {
                    textBox13.Text = t.ToString("N3");
                }
                if (validHmax)
                {
                    textBox14.Text = hmax.ToString("N3");
                }
            }
            else
            {
                // Changing button state
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;

                button2.Text = "Find";
                button2.BackColor = Color.PaleGreen;

                // Reset color
                textBox6.ForeColor = SystemColors.WindowText;
                textBox7.ForeColor = SystemColors.WindowText;
                textBox8.ForeColor = SystemColors.WindowText;
                textBox9.ForeColor = SystemColors.WindowText;

                // Clearing value in textbox
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";

                // Default units (SI)
                comboBox6.SelectedIndex = 0;
                comboBox7.SelectedIndex = 0;
                comboBox8.SelectedIndex = 0;
                comboBox9.SelectedIndex = 0;
                comboBox10.SelectedIndex = 0;
                comboBox11.SelectedIndex = 0;
                comboBox12.SelectedIndex = 0;
                comboBox13.SelectedIndex = 0;
                comboBox14.SelectedIndex = 0;
            }
        }
    }
}