require "./genlib"

page = {}
page[:file] = "standalone_mist"
page[:title] = "Standalone Mist"

page[:quote] = <<EOF
<p>
  <quote>
    "Lisp doesn't look any deader than usual to me."<br/>
    - David Thornley, reply to a question older than most languages
  </quote>
</p>
EOF

page[:content] = <<EOF

<h1>Standalone Mist</h1>
<p>
  TODO
</p>

EOF

generate page
