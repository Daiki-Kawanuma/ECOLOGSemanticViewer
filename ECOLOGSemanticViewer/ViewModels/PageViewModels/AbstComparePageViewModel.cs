﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using ECOLOGSemanticViewer.Models;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public abstract class AbstComparePageViewModel : ViewModel
    {

        public abstract void CreatePlotModel();

        public abstract void CreatePercentilePlotModel();

        public abstract void CreateCalculatedPlotModel(int numberOfTrip);

    }
}
