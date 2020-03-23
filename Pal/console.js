mergeInto(LibraryManager.library, {
    js_build_table: function(width, height)
    {
        var board = document.getElementById("board");
        var s = "<table style='background-color: black;border:1px solid red;'>";
        for(var i = 0; i < height; i++)
        {
            s = s + "<tr>";
            for(var j = 0; j < width; j++)
            {
                s = s + "<td id='c" + i + "_" + j + "'>&nbsp;</td>";
            }
            s = s + "</tr>";
        }
        s = s + "</table>";
        board.innerHTML = s;
    },
    js_write_char: function (x, y, c, color) {
        var cell = document.getElementById("c" + y + "_" + x);
        cell.style.color = colorMap[color];
        cell.innerHTML = String.fromCharCode(c);
    },
    js_key_available: function () {
        return lastKey;
    },
    js_read_key: function () {
        var k = lastKey;
        lastKey = 0;
        return k;
    },
    js_set_title: function (s) {
        var t = UTF16ToString(s);
        document.title = t;
    }
});
