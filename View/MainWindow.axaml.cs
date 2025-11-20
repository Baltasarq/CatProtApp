// CatProtApp (c) 2025 Baltasar MIT License <jbgarcia@uvigo.es>


namespace CatProtApp.View;


using System;
using System.Diagnostics;
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
        this._cats = new CatCollection();
        this.Update();
        Trace.Listeners.Add( new ConsoleTraceListener() );
    }

    /// <summary>Updates the view.</summary>
    private void Update()
    {
        var edName = this.GetControl<TextBox>( "EdName" );
        var edRace = this.GetControl<ComboBox>( "EdRace" );
        var edBirthDate = this.GetControl<DatePicker>( "EdBirthDate" );
        var lblStatus = this.GetControl<Label>( "LblStatus" );
        //int pos = Math.Min( this._cats.Count, this._cats.Cursor + 1 );


        // Status
        lblStatus.Content = "0 / 0";

        Trace.WriteLine( $"Cursor: {this._cats.Cursor} / {this._cats.Count}" );
        Trace.WriteLine( $"IsCursorValid: {this._cats.IsCursorValid()}" );

        // Current cat
        if ( this._cats.IsCursorValid() ) {
            var cat = this._cats.GetAtCursor();

            edName.Text = cat.Name;
            edRace.SelectedIndex = (int) cat.Race;
            edBirthDate.SelectedDate = new DateTimeOffset( cat.Birth );
            lblStatus.Content = $"{this._cats.Cursor + 1} / {this._cats.Count}";
        }
    }

    /// <summary>Change to the next element, if it exists.</summary>
    private void Next()
    {
        this._cats.Next();
        this.Update();
    }

    /// <summary>Change to the previous element, if it exists.</summary>
    private void Prev()
    {
        this._cats.Prev();
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

    private readonly CatCollection _cats;
}
