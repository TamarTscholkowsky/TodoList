﻿using System;
using System.Collections.Generic;

namespace TodoApi;

public partial class Tasks
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsComplete { get; set; }
}
