namespace WebPConverter.Code {
    public class JSettings {
        #region WebP Settings
        public bool Lossless { get; set; }
        public bool RemoveAlpha { get; set; }
        public bool EmulateJpeg { get; set; }
        public string Quality { get; set; }
        #endregion

        #region Software Settings
        public bool DeleteImage { get; set; }
        public bool PlaySound { get; set; }
        public int SoundToPlay { get; set; }
        #endregion

    }

}
