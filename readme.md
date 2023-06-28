

---

# DlgExtract

A command line utility to read dialog file from any *.dlg files and string information from dialog.tlk files used Infinity Engine to extract as text files.

## Usage
``` 
dlgextract c:/resource
```

The resource folder must contains the TLK and DLG files.
The tool will create text files in c:/resource/TXT folder.

To extract the DLG file from the game you must use [Near Infinity Browser](https://github.com/NearInfinityBrowser/NearInfinity) or any other similar tool that can extract DLG file from BIF files.


## Technologies

DlgExtract is written in C# Net 7.0.


## Compiling

To clone and run this application, you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

```
# Clone this repository
$ git clone https://github.com/B4st13n/DLG_Extract
```
 
## License

The code has been created as a Fork of
[TlkToSql](https://github.com/btigi/tlktosql), a command line utility to read string information from dialog.tlk files used Infinity Engine to an SQLite database
and is licensed under [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)

