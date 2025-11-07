namespace BackendChallenge.Application.Common
{
    public static class ImageValidator
    {
        public static bool TryDecodeBase64(string base64, out byte[] bytes)
        {
            try { bytes = Convert.FromBase64String(base64); return true; }
            catch { bytes = Array.Empty<byte>(); return false; }
        }

        // Validação por magic bytes
        public static string? DetectContentType(ReadOnlySpan<byte> bytes)
        {
            // PNG: 89 50 4E 47 0D 0A 1A 0A
            if (bytes.Length >= 8 &&
                bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 &&
                bytes[4] == 0x0D && bytes[5] == 0x0A && bytes[6] == 0x1A && bytes[7] == 0x0A)
                return "image/png";

            // BMP: 42 4D
            if (bytes.Length >= 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
                return "image/bmp";

            return null;
        }
    }
}
