# osu! MP3 Extractor

A command-line tool for extracting MP3 audio files from osu! beatmaps. This utility allows you to easily extract and organize music files from your osu! installation by specifying beatmap IDs.

## What is osu!?

[osu!](https://osu.ppy.sh/) is a popular free-to-play rhythm game where players click, slide, and spin to the beat of the music. Each beatmap contains both the game data and audio files (typically MP3s). This tool helps you extract just the audio files for listening outside of the game.

## Features

- Extract MP3 files from osu! beatmaps by beatmap ID
- Support for bulk extraction using multiple beatmap IDs
- Input beatmap IDs via command line or JSON file
- Automatic file naming using beatmap names
- Preserves original audio quality
- Cross-platform support (.NET 8.0)

## Requirements

- .NET 8.0 Runtime or SDK
- An existing osu! installation with downloaded beatmaps
- Write permissions to the destination directory

## Installation

### Option 1: Build from Source

1. Clone this repository:
   ```bash
   git clone https://github.com/nayutalienx/osu-mp3-extracter.git
   cd osu-mp3-extracter
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run -- [options]
   ```

### Option 2: Build Executable

1. Build a self-contained executable:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained
   ```
   
   Replace `win-x64` with your target platform:
   - `win-x64` for Windows 64-bit
   - `linux-x64` for Linux 64-bit  
   - `osx-x64` for macOS 64-bit

2. The executable will be in `bin/Release/net8.0/[runtime]/publish/`

## Usage

### Basic Command Structure

```bash
osu-mp3-extractor.exe -osu=<osu_path> -destination=<dest_path> [options]
```

### Command Line Options

| Option | Description | Required | Example |
|--------|-------------|----------|---------|
| `-osu=<path>` | Path to your osu! installation directory | Yes | `-osu=C:\Users\user\AppData\Local\osu!` |
| `-destination=<path>` | Directory where extracted MP3s will be saved | Yes | `-destination=D:\osu-music` |
| `-ids=<id1,id2,id3>` | Comma-separated list of beatmap IDs | * | `-ids=1234,5678,9012` |
| `-input=<file.json>` | JSON file containing array of beatmap IDs | * | `-input=beatmaps.json` |
| `-help` | Display usage information | No | `-help` |

\* Either `-ids` or `-input` must be provided

### Examples

#### Extract specific beatmaps by ID:
```bash
osu-mp3-extractor.exe -osu=C:\Users\user\AppData\Local\osu! -destination=D:\my-music -ids=372245,774965,1618914
```

#### Extract beatmaps from JSON file:
```bash
osu-mp3-extractor.exe -osu=C:\Users\user\AppData\Local\osu! -destination=D:\my-music -input=my-beatmaps.json
```

#### Get help:
```bash
osu-mp3-extractor.exe -help
```

### JSON Input File Format

Create a JSON file containing an array of beatmap IDs as strings:

```json
[
  "372245",
  "774965", 
  "1618914",
  "2065533"
]
```

### Finding Beatmap IDs

Beatmap IDs can be found in several ways:

1. **From osu! website**: The ID is in the URL (e.g., `https://osu.ppy.sh/beatmapsets/372245` → ID: `372245`)
2. **From osu! client**: Check the beatmap folder names in your osu! Songs directory
3. **From .osu files**: The filename or folder name contains the beatmap ID

### Output

- Extracted MP3 files are saved to the destination directory
- Files are named using the beatmap's title when available
- If no title is found, the beatmap ID is used as the filename
- The tool displays progress information for each file copied

Example output:
```
File C:\Users\user\AppData\Local\osu!\Songs\372245 DragonForce - Symphony of the Night\audio.mp3 copied to D:\my-music\DragonForce - Symphony of the Night.mp3
```

## Common Issues

### "You need pass all directories"
Make sure both `-osu` and `-destination` parameters are provided with valid paths.

### "You need pass beatmaps ids"
Provide either `-ids` with comma-separated beatmap IDs or `-input` with a JSON file.

### File copy errors
- Ensure the destination directory exists and is writable
- Check that the osu! installation path is correct
- Verify that the specified beatmaps are actually downloaded in your osu! installation

## Technical Details

- **Language**: C# (.NET 8.0)
- **Dependencies**: Newtonsoft.Json for JSON parsing
- **Architecture**: Console application with command-line argument parsing
- **File Operations**: Uses System.IO for file copying and directory traversal

## Contributing

Feel free to submit issues, feature requests, or pull requests to improve this tool.

## License

This project is open source. Please check the repository for license details.

---

**Note**: This tool is for personal use with legally obtained beatmaps from your own osu! installation. Respect the original artists and creators of the music.
