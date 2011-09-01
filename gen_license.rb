require "./genlib"

page = {}
page[:file] = "license"
page[:title] = "MIST License"

page[:quote] = <<EOF
<p class="quote">
    "We were not out to win over<br/>the Lisp programmers; we were after the C++ programmers.<br/>We managed to drag a lot of them about halfway to Lisp."<br/>
    - Guy Steele, Java spec co-author
</p>
EOF

page[:content] = <<EOF

<h1>Microsoft Public License (MS-PL)</h1>

<p>This license governs use of the accompanying software. If you use the software, you
accept this license. If you do not accept the license, do not use the software.</p>

<h2>1. Definitions</h2>
<p>The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
same meaning here as under U.S. copyright law.<br/>
A "contribution" is the original software, or any additions or changes to the software.<br/>
A "contributor" is any person that distributes its contribution under this license.<br/>
"Licensed patents" are a contributor's patent claims that read directly on its contribution.</p>

<h2>2. Grant of Rights</h2>
<p>(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.<br/>
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.</p>

<h2>3. Conditions and Limitations</h2>
<p>(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.<br/>
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.<br/>
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.<br/>
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.<br/>
(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.</p>


EOF

generate page
