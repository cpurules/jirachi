﻿using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public interface ISaveFileHandler {
        void WriteSaveFile(string filePath);
        GameModel ReadSaveFile();
    }
}
