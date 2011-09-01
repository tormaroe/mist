require "./genlib"

page = {}
page[:file] = "index"
page[:title] = "The MIST Programming Language"

page[:quote] = <<EOF
<p class="quote">
    "Within a couple weeks of learning Lisp I found programming<br/>in any other language unbearably constraining."<br/>
    - <a href="http://www.paulgraham.com/">Paul Graham</a>
</p>
EOF

page[:content] = <<EOF

<h1>Mist is...</h1>
<p>
  .. a general purpose, dynamic, functional programming language, and a member of the Lisp family of languages. It runs on the CLR / .Net Framework on a high-level interpreter implemented in C#.
</p>
<p style="color:red;">
  Mist is in early alpha, but a beta planned for October/November 2011. This site and the Mist documentation is work in progress. Follow the progress on <a href="https://github.com/tormaroe/mist">Github</a>!
</p>

EOF

generate page
