require "./genlib"

page = {}
page[:file] = "embedded_mist"
page[:title] = "Embedded Mist"

page[:quote] = <<EOF
<p>
  <quote>
    "Programming in Lisp is like playing with the primordial forces<br/>
    of the universe. It feels like lightning between your fingertips.<br/>
    No other language even feels close."<br/>
    <a href="http://cooking-with-lisp.blogspot.com/">Glenn Ehrlich</a>
  </quote>
</p>
EOF

page[:content] = <<EOF

<h1>Embedded Mist</h1>
<p>
  TODO
</p>

EOF

generate page
