﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.ProjectEditors
{
    public interface IProjectEditor
    {
        void LoadFromProject(Project project);
        void ApplyToProject(Project project);
    }
}
