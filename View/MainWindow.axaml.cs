// CatProtApp (c) 2025 Baltasar MIT License <jbgarcia@uvigo.es>


namespace CatProtApp.View;


using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Core;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();

        // Lookup controls
        var edRace = this.GetControl<ComboBox>( "EdRace" );
        var btNext = this.GetControl<Button>( "BtNext" );
        var btPrev = this.GetControl<Button>( "BtPrev" );
        var btNew = this.GetControl<Button>( "BtNew" );

        // Set the items of the combobox
        edRace.ItemsSource = Cat.RaceLabels;

        // Button action callbacks
        btNext.Click += (_, _) => this.Next();
        btPrev.Click += (_, _) => this.Prev();
        btNew.Click += (_, _) => this.New();

        // Goto 0
        this._pos = 0;
        this.Update();
    }

    /// <summary>Updates the view.</summary>
    private void Update()
    {
        var edName = this.GetControl<TextBox>( "EdName" );
        var edRace = this.GetControl<ComboBox>( "EdRace" );
        var edBirthDate = this.GetControl<DatePicker>( "EdBirthDate" );
        var lblStatus = this.GetControl<Label>( "LblStatus" );
        int numCats = this._cats.Count;
        int pos = Math.Max( 0, Math.Min( numCats, this._pos + 1 ) );

        // Status
        lblStatus.Content = $"{pos} / {numCats}";

        // Current cat
        if ( this._pos < numCats ) {
            var cat = this._cats[ this._pos ];

            edName.Text = cat.Name;
            edRace.SelectedIndex = (int) cat.Race;
            edBirthDate.SelectedDate = new DateTimeOffset( cat.Birth );
        }
    }

    /// <summary>Change to the next element, if it exists.</summary>
    private void Next()
    {
        this._pos = Math.Min( this._cats.Count - 1, this._pos + 1 );
        this.Update();
    }

    /// <summary>Change to the previous element, if it exists.</summary>
    private void Prev()
    {
        this._pos = Math.Max( this._pos - 1, 0 );
        this.Update();
    }

    /// <summary>Creates a new cat, and adds it to the list.</summary>
    private void New()
    {
        var edName = this.GetControl<TextBox>( "EdName" );
        var edRace = this.GetControl<ComboBox>( "EdRace" );
        var edBirthDate = this.GetControl<DatePicker>( "EdBirthDate" );

        var race = Enum.GetValues<Cat.Races>()[ edRace.SelectedIndex ];
        var date = edBirthDate.SelectedDate.GetValueOrDefault(
                                                    new DateTimeOffset( DateTime.Today ));
        var cat = new Cat{  Name = edName.Text ?? "Garfield",
                            Race = race,
                            Birth = date.Date };
        this._cats.Add( cat );
        this.Update();
    }

    private readonly IList<Cat> _cats = [];
    private int _pos;
}
