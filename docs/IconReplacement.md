# Replacing Certificate and Mastery Icons

## Icon Locations

All icons are located in `src/EVEMon.Common/Resources/Images/`.

## Certificate Level Icons

Used in the Certificate Browser to indicate certificate completion level.

| File | Description |
|------|-------------|
| `level0_24.png` | Level 0 - Untrained (faded) |
| `level1_24.png` | Level 1 (Basic) - Untrained |
| `level2_24.png` | Level 2 (Standard) - Untrained |
| `level3_24.png` | Level 3 (Improved) - Untrained |
| `level4_24.png` | Level 4 (Advanced) - Untrained |
| `level5_24.png` | Level 5 (Elite) - Untrained |
| `level0_24_1.png` | Level 0 - Trained (filled) |
| `level1_24_1.png` | Level 1 (Basic) - Trained |
| `level2_24_1.png` | Level 2 (Standard) - Trained |
| `level3_24_1.png` | Level 3 (Improved) - Trained |
| `level4_24_1.png` | Level 4 (Advanced) - Trained |
| `level5_24_1.png` | Level 5 (Elite) - Trained |
| `Certificate-24.png` | Generic certificate category icon |

## Mastery Level Icons

Used in the Mastery Browser to indicate ship mastery level.

| File | Description |
|------|-------------|
| `masteryLevel0_24.png` | No mastery - Untrained (faded) |
| `masteryLevel1_24.png` | Mastery Level 1 - Untrained |
| `masteryLevel2_24.png` | Mastery Level 2 - Untrained |
| `masteryLevel3_24.png` | Mastery Level 3 - Untrained |
| `masteryLevel4_24.png` | Mastery Level 4 - Untrained |
| `masteryLevel5_24.png` | Mastery Level 5 - Untrained |
| `masteryLevel0_24_1.png` | No mastery - Trained (filled) |
| `masteryLevel1_24_1.png` | Mastery Level 1 - Trained |
| `masteryLevel2_24_1.png` | Mastery Level 2 - Trained |
| `masteryLevel3_24_1.png` | Mastery Level 3 - Trained |
| `masteryLevel4_24_1.png` | Mastery Level 4 - Trained |
| `masteryLevel5_24_1.png` | Mastery Level 5 - Trained |

## Icon States

- **Untrained (base files)**: Faded/outline appearance, shown when the level has NOT been achieved
- **Trained (`_1` suffix)**: Filled/bright appearance, shown when the level HAS been achieved

## Replacing Icons

1. Create your replacement icons as **24x24 pixel PNGs** with transparency
2. Replace the corresponding files in `src/EVEMon.Common/Resources/Images/`
3. Ensure both variants exist (base and `_1` suffix) for consistent appearance
4. Verify files are set as embedded resources in `EVEMon.Common.csproj`:
   ```xml
   <EmbeddedResource Include="Resources\Images\yourfile.png" />
   ```
5. Rebuild the project

## Technical Requirements

- **Format:** PNG with alpha transparency
- **Size:** 24x24 pixels
- **Color Depth:** 32-bit ARGB (for proper transparency)
- **Naming:** Follow exact naming pattern - code references files by name

## File Naming Convention

| Pattern | Usage |
|---------|-------|
| `level{N}_24.png` | Certificate level N, untrained state |
| `level{N}_24_1.png` | Certificate level N, trained state |
| `masteryLevel{N}_24.png` | Mastery level N, untrained state |
| `masteryLevel{N}_24_1.png` | Mastery level N, trained state |

Where `{N}` is a number from 0 to 5.
