## Background
	This code was adapted from rvrsh3ll's FileSplitter.ps1: https://github.com/rvrsh3ll/Misc-Powershell-Scripts/blob/master/FileSplitter.ps1
	The C# parser for this tool was directly copied from HarmJ0y's Rubeus: https://github.com/GhostPack/Rubeus
512

### Command Line Usage

    File Splitter:

        Split a file into chunks based on a specified size in bytes.
            FileSplitter.exe /input:full_path_to_file_to_split /outdir:full_path_to_directory_to_output_chunks /name:name_of_chunk /size:size_in_bytes
        	Required Values: input and outdir
        	Default Values: name = "chunk", size = "1024"

            Ex. Splitting c:\users\username\desktop\file.iso [2GB file]
            FileSplitter.exe /input:c:\users\username\desktop\file.iso /outdir:c:\users\username\desktop\test /size:536870912

            The command above will turn the 2GB file.iso into four 512MB files called chunk1, chunk2, chunk3, chunk4

    File Combiner:

        Combine a file that was split into chunks by FileSplitter.exe:
            FileCombiner.exe /input:full_path_to_input_directory /outdir:full_path_to_output_diectory /outfile:name_of_output_file /prefix:name_of_chunk
            Required Values: input, outdir, outfile
            Default Values: name = "chunk"

            Ex. Combining 4 chunks with the name "file" in the c:\users\username\desktop\test directory
            FileCombiner.exe /input:c:\users\username\desktop\test /outdir:c:\users\username\desktop /outfile:myfile.exe /name:file

            The command above will combine all chunks named "file" in the c:\users\username\desktop\test directory and output a file called "myfile.exe" on the user's desktop.
