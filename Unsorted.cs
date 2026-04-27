using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    public static class Game
    {
        public static int CurrentFrame { get => pCurrentFrame.Convert<int>().Data; set => pCurrentFrame.Convert<int>().Ref = value; }
        public static IntPtr pCurrentFrame = new IntPtr(0xA8ED84); // you should not change it

        // The height in the middle of a cell with a slope of 30 degrees
        public const int LevelHeight = 104;//89DE70
        public const int BridgeLevels = 4;
        public const int BridgeHeight = LevelHeight * BridgeLevels;//ABC5DC

        public const int CellSize = 256;

        public static int IKnowWhatImDoing { get => iKnowWhatImDoing.Convert<int>().Data; set => iKnowWhatImDoing.Convert<int>().Ref = value; }
        private static IntPtr iKnowWhatImDoing = new IntPtr(0xA8E7AC); // you should not change it

        public static int CurrentSWType { get => currentSWType.Convert<int>().Data; set => currentSWType.Convert<int>().Ref = value; }
        private static IntPtr currentSWType = new IntPtr(0x8809A0);
        private static readonly Pointer<uint> _currentFrameRate = new IntPtr(0xABCD44);
        private static readonly Pointer<uint> _totalFramesElapsed = new IntPtr(0xABCD48);
        private static readonly Pointer<uint> _totalTimeElapsed = new IntPtr(0xABCD4C);
        private static readonly Pointer<bool> _reducedEffects = new IntPtr(0xABCD50);

        // 实时 FPS（每次访问都会读取最新值，不是固定值）
        public static uint CurrentFrameRate => _currentFrameRate.IsNotNull ? _currentFrameRate.Ref : 0;
        
        // 总帧数
        public static uint TotalFramesElapsed => _totalFramesElapsed.IsNotNull ? _totalFramesElapsed.Ref : 0;
        
        // 总耗时（单位：毫秒 / 秒 根据游戏决定）
        public static uint TotalTimeElapsed => _totalTimeElapsed.IsNotNull ? _totalTimeElapsed.Ref : 0;
        
        // 低帧率模式
        public static bool ReducedEffects => _reducedEffects.IsNotNull && _reducedEffects.Ref;
            
        public static double GetAverageFrameRate()
        {
            if(TotalTimeElapsed > 0) { //防止除0
                return (double)TotalFramesElapsed
                    / TotalTimeElapsed;
            }

            return 0.0;
        }
        

        public static unsafe bool HasDirtyArea()
        {
            var func = (delegate* unmanaged[Stdcall]<Bool>)0x53BAE0;
            return func();
        }
    }
}
