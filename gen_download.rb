require "./genlib"

page = {}
page[:file] = "download"
page[:title] = "Download Mist"

page[:quote] = <<EOF
<p class="quote">
    "Language designers are not intellectuals.<br/>
    They're not as interested in thinking as you might hope.<br/>
    They just want to get a language done and start using it."<br/>
    - Dave Moon
</p>
EOF

page[:content] = <<EOF

<h1>Download Mist</h1>
<p>
  No downloads here yet! Get the source from <a href="https://github.com/tormaroe/mist">Github</a> if you're interested.
</p>

EOF

generate page
