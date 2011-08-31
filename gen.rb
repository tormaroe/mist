if __FILE__ == $0
  Dir.foreach(".") {|f| f.match(/gen_/) { load f } }
end
