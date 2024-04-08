// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;

namespace ScratchProject;



// As we can't currently design in VS in the runtime solution, mark as "Default" so this opens in code view by default.
[DesignerCategory("Default")]
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.Load += Form1_Load;

    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        var button = new Button()
        {
            Text = "Open"
        };

        button.Click += Button_Click;
        this.Controls.Add(button);
    }

    private void Button_Click(object? sender, EventArgs e)
    {
        this.IsMdiContainer = true;

        var form = new Form();
        var button = new Button();

        var a = this.MdiChildren;
        form.Controls.Add(button);
        form.Show();
        form.MdiParent = this;

        form.LocationChanged += Form_LocationChanged;
        form.SizeChanged += Form_SizeChanged;
    }

    private void Form_SizeChanged(object? sender, EventArgs e)
    {
        var meForm = sender as Form;
        if (meForm is null)
            return;

        foreach (Form form in Application.OpenForms)
        {
            if (form != meForm)
            {
                if (Math.Abs(form.Left - meForm.Right) < MagneticDistance)
                {
                    meForm.Left = form.Left - meForm.Width;
                    break;
                }
                else if (Math.Abs(form.Right - meForm.Left) < MagneticDistance)
                {
                    meForm.Left = form.Right;
                    break;
                }
                else if (Math.Abs(form.Top - meForm.Bottom) < MagneticDistance)
                {
                    meForm.Top = form.Top - meForm.Height;
                    break;
                }
                else if (Math.Abs(form.Bottom - meForm.Top) < MagneticDistance)
                    meForm.Top = form.Bottom;
            }
        }
    }

    private const int MagneticDistance = 7; // Adjust this value as needed

    private void Form_LocationChanged(object? sender, EventArgs e)
    {
        var meForm = sender as Form;
        if (meForm is null)
            return;

        foreach (Form form in Application.OpenForms)
        {
            if (form != meForm)
            {
                if (Math.Abs(form.Left - meForm.Right) < MagneticDistance)
                    meForm.Left = form.Left - meForm.Width;
                else if (Math.Abs(form.Right - meForm.Left) < MagneticDistance)
                    meForm.Left = form.Right;
                else if (Math.Abs(form.Top - meForm.Bottom) < MagneticDistance)
                    meForm.Top = form.Top - meForm.Height;
                else if (Math.Abs(form.Bottom - meForm.Top) < MagneticDistance)
                    meForm.Top = form.Bottom;
            }
        }
    }
}
