using CommandLine;
using CommandLine.Text;
using PD2.ConsoleUtils;
using PD2.GameSave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PD2
{
	static class Program
	{
		class Options
		{
			[Option('e', "encryptFile", DefaultValue = false, HelpText = "Encrypt save file. Enable this if you are encrypting an already unencrypted file.")]
			public bool EncryptOutput { get; set; }

			[Option('o', "outputPath", DefaultValue = "output.sav", HelpText = "Path to save the processed game save to.")]
			public String OutputPath { get; set; }

			[Option('i', "inputPath", Required = true, HelpText = "Path to the PAYDAY 2 game save that you'd like to process.")]
			public String InputPath { get; set; }

            [Option('m', "editorMode", DefaultValue = false, HelpText = "Open the editor to edit the Save File and saves to output file encrypted.")]
            public bool EditorMode { get; set; }

            [HelpOption]
			public string GetUsage()
			{
				return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
			}
		}

		static void Main(String[] args)
		{
			var options = new Options();

			if (CommandLine.Parser.Default.ParseArguments(args, options))
			{
                ConsoleLogging.ShowTitle();

                if (!File.Exists(options.InputPath))
				{
					ConsoleLogging.Log("Input file doesn't exist. Are you sure you typed the filepath right?", LogLevel.Error);
					return;
				}
				else if (options.InputPath.Equals(options.OutputPath))
				{
                    ConsoleLogging.Log("Input file is equal to Output file, it's safer to have two different files!", LogLevel.Error);
                    return;
                }

				try
				{
					SaveFile file = new SaveFile(options.InputPath, !options.EncryptOutput);
					if (options.EditorMode)
					{
						ConsoleLogging.Log("Opening Editor...", LogLevel.Info);
						Thread.Sleep(1000);
						DictionaryEditor.Open(file.GameDataBlock.Dictionary);
                        options.EncryptOutput = true;
                    }
					file.Save(options.OutputPath, options.EncryptOutput);
                    ConsoleLogging.Log($"Saved the modded/decrypted Save File at '{options.OutputPath}'", LogLevel.Info);
				}
				catch (Exception e) { 
					ConsoleLogging.Log($"An unknown error has occured: {e.Message}", LogLevel.Error);
				}
			}
		}
	}
}