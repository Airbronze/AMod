using System;
using System.Collections.Generic;
using UnityEngine;

public enum PathfindingResult : byte
{
    SUCCESSFUL,
    CANCELLED,
    ERROR_START_OUT_OF_BOUNDS,
    ERROR_END_OUT_OF_BOUNDS,
    Same_Block,
    ERROR_PATH_TOO_LONG,
    Start_Not_Valid,
    Invalid_Ending_Pos,
    Path_Not_Found,
    Path_Not_Found_Block
}