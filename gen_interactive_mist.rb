require "./genlib"

page = {}
page[:file] = "interactive_mist"
page[:title] = "Interactive Mist"

page[:quote] = <<EOF
<p class="quote">
    "SQL, Lisp, and Haskell are the only programming languages that I've seen where one spends more time thinking than typing."<br/>
    - Philip Greenspun
</p>
EOF

page[:content] = <<EOF

<h1>Interactive Mist: Using the REPL</h1>
<p>
  TODO: Screenshot, explain usage, when to use REPL
</p>
<img src="img/REPL1.png" alt="Mist REPL Screenshot"/>

<h2>REPL-only functions and variables</h2>
<p>
  TODO: quit, reset, memory variables
</p>

<h2>User file</h2>
<p>
  TODO
</p>

EOF

generate page
