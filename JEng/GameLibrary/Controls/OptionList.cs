// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.Controls
{
    /// <summary>
    /// Allows the choice of several options.
    /// </summary>
    public class OptionList : List<Control>
    {
        private Control control;
        private Label dataLabel;
        private LinkLabel increment;
        private LinkLabel decrement;
        private string[] data;
        private int index;
        private int largestWidth;
        private bool outline = false;
        private int outlineWidth = 2;


        public Control Control
        {
            get { return control; }
            set { control = value; }
        }

        public string Text
        {
            get { return data[index]; }
        }

        public bool Outline
        {
            get { return outline; }
            set { outline = value; }
        }

        public int OutlineWidth
        {
            get { return outlineWidth; }
            set { outlineWidth = value; }
        }

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                ResetLabels();
            }
        }

        public OptionList(Control control, string[] data, int spacing)
        {
            index = 0;
            this.control = control;
            this.data = data;

            outline = true;
            outlineWidth = 3;

            dataLabel = new Label()
            {
                Text = data[index],
                Position = control.Position,
                Color = control.Color,
                SpriteFont = control.SpriteFont,
                Outline = outline
            };

            // Calculate the largest item, so the arrows can be positioned to never overlap.
            largestWidth = 0;
            int indexOfLargest = 0;

            for (int i = 0; i < data.Length; i++)
            {
                int calculatedWidth = (int)control.SpriteFont.MeasureString(data[i]).X;
                largestWidth = calculatedWidth > largestWidth ? calculatedWidth : largestWidth;
                indexOfLargest = calculatedWidth >= largestWidth ? i : indexOfLargest;
            }
            
            // Set the longest string visible, set region, then set text back to default
            dataLabel.Text = data[indexOfLargest];
            dataLabel.SetRegion();
            dataLabel.Text = data[0];

            Add(dataLabel);

            increment = new LinkLabel()
            {
                Text = ">",
                Position = dataLabel.Position + new Vector2(dataLabel.Region.Width + spacing, 0),
                Color = control.Color,
                SpriteFont = control.SpriteFont,
                SelectedColor = Color.DarkGray,
                Outline = outline
            };
            increment.Selected += Increment_Clicked;
            increment.SetRegion();
            Add(increment);

            decrement = new LinkLabel()
            {
                Text = "<",
                Position = dataLabel.Position - new Vector2(increment.Region.Width + spacing, 0),
                Color = control.Color,
                SpriteFont = control.SpriteFont,
                SelectedColor = Color.DarkGray,
                Outline = outline
            };
            decrement.Selected += Decrement_Clicked;
            decrement.SetRegion();
            Add(decrement);

            ResetLabels();
        }

        private void Decrement_Clicked(object sender, EventArgs eventArgs)
        {
            index--;
            if (index == -1)
                index = 0;
            dataLabel.Text = data[index];

            ResetLabels();
        }

        private void Increment_Clicked(object sender, EventArgs eventArgs)
        {
            index++;
            if (index == data.Length)
                index--;
            dataLabel.Text = data[index];

            ResetLabels();
        }

        private void ResetLabels()
        {
            // Calculate the middle of the object, and centre the label.
            // Makes it look nicer.
            int widthOfLabel = (int)control.SpriteFont.MeasureString(data[index]).X;
            Vector2 startPoint = control.Position;
            Vector2 newPosition = startPoint;
            newPosition.X += (largestWidth - widthOfLabel) / 2f;
            dataLabel.Position = newPosition;

            dataLabel.Text = data[index];

            // Grey out the arrows if can't be used.
            increment.Color = index == data.Length - 1 ? increment.SelectedColor : Color.White;
            decrement.Color = index == 0 ? increment.SelectedColor : Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control controlComponent in this)
            {
                controlComponent.Draw(spriteBatch);
            }
        }

        public void HandleInput()
        {
            foreach (Control controlComponent in this)
            {
                controlComponent.HandleInput();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Control controlComponent in this)
            {
                controlComponent.Update(gameTime);
            }
        }
    }
}
