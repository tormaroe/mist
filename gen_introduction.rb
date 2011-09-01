require "./genlib"

page = {}
page[:file] = "introduction"
page[:title] = "Introduction to MIST"

page[:quote] = <<EOF
<p class="quote">
    "Programming in Lisp is like playing with the primordial forces<br/>
    of the universe. It feels like lightning between your fingertips.<br/>
    No other language even feels close."<br/>
    <a href="http://cooking-with-lisp.blogspot.com/">Glenn Ehrlich</a>
</p>
EOF

page[:content] = <<EOF
<h1>An Introduction to Mist</h1>
<p>
  bla bla bla
</p>
EOF

generate page
