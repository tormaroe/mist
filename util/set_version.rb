=begin

  Util to change version number (major and minor) in all
  the .NET projects in one single stroke.

  Just run it with the wanted version as an argument, like:
  set_version.rb 0.1

  Format is validated before used, so ding something wrong
  is not easy...

=end

VERSION_FILES = [
  '..\\src\\REPL\\Properties\\AssemblyInfo.cs',
  '..\\src\\PACKER\\Properties\\AssemblyInfo.cs',
  '..\\src\\Marosoft.Mist\\Properties\\AssemblyInfo.cs',
  '..\\test\\Properties\\AssemblyInfo.cs'
]

def read file
  buffer = ''
  File.open(file, "r") do |file|
    while (line = file.gets)
      buffer += line
    end
  end  
  return buffer
end

def gsub_assemblytag! tag, text, value
  raise "INVALID VERSION FORMAT" unless value =~ /^\d+.\d+$/
  text.
    gsub!(/\[assembly: #{tag}\("\d+.\d+.\*"\)\]/, 
         %Q<[assembly: #{tag}("#{value}.*")]>)
end

def set_version! v, file_content
  gsub_assemblytag!("AssemblyVersion", file_content, v)
  gsub_assemblytag!("AssemblyFileVersion", file_content, v)
end

def save name, content
  File.open(name, "w") { |f| f.puts content }
end

if ARGV != nil and ARGV.length == 1
  for vfile in VERSION_FILES
    puts "Setting version #{ARGV[0]} in #{vfile}"
    file = read( File.dirname(__FILE__) + "\\" + vfile)
    set_version!(ARGV[0], file)
    save File.dirname(__FILE__) + "\\" + vfile, file
  end
else
  puts "Specify version number, format: major.minor"
end
