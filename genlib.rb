def template
  template = ''
  File.open('template.html', "r") do |file|
    while (line = file.gets)
      template += line
    end
  end  
  return template
end

def generate args
  name = args[:file] + ".html"
  File.open(name, "w") do |file|
    file.puts template.
      gsub(/\{CONTENT\}/, args[:content]).
      gsub(/\{QUOTE\}/, args[:quote]).
      gsub(/\{TITLE\}/, args[:title])
  end
  puts "Generated file #{name}"
end

