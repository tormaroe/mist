require "./genlib"

page = {}
page[:file] = "api"
page[:title] = "Mist API"

page[:quote] = <<EOF
<p class="quote">
    "Lisp is a programmer amplifier."<br/>
    - Martin Rodgers
</p>
EOF

page[:content] = <<EOF

<h1>Mist API</h1>
<p>
 Todo: <i>At the top there should be a sorted list of all functions / keywords with links to the detailed descriptions further down the page.</i> 
</p>

EOF

generate page
