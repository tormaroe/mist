require "./genlib"

page = {}
page[:file] = "api"
page[:title] = "Mist API"

page[:quote] = <<EOF
<p>
  <quote>
    "Lisp is a programmer amplifier."<br/>
    - Martin Rodgers
  </quote>
</p>
EOF

page[:content] = <<EOF

<h1>Mist API</h1>
<p>
  Todo
</p>

EOF

generate page
