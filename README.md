PAYDAY 2 Save Editor
===================
This is a save editor for the PAYDAY 2 video game, it currently only supports the retail version on PC.  All data types, encryption, hashing, and encoding have been reversed and re-implemented. This tool can rebuild the entire save file and it's data.  

You may download the latest version here: https://github.com/capelosini/PD2SE/releases

**~~A fork that includes a UI may be found here: https://github.com/isqrl/PayDay2SaveView (credit to [@isqrl](https://github.com/isqrl))~~**

Usage:

    // decrypt save file
    PD2SE.exe -i save000.sav -o output.sav 
    
    // re-encrypt save file
    PD2SE.exe -i output.sav -o newsave.sav -e 
    
    // open the editor
    PD2SE.exe -i save000.sav -o output.sav -m

    // open a decrypted save file in the editor
    PD2SE.exe -i output.sav -o newsave.sav -m -e

    // display help
    PD2SE.exe --help 
