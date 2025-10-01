// protapp (c) 2025 Baltasar MIT License <jbgarcia@uvigo.es>


namespace CatProtApp.Core;


using System;
using System.Collections.Generic;


public class Cat {
    public enum Races { CommonEuropean, Siamese, Persian, Sphinx }

    public static readonly IReadOnlyList<string> RaceLabels = [
                            "Común europeo", "Siamés", "Persa", "Esfinge" ];

    public required string Name { get; init; }
    public required Races Race { get; init; }
    public required DateTime Birth { get; init; }
}
