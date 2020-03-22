mergeInto(LibraryManager.library, {
js_build_table: function(width, height)
{
    var board = document.getElementById("board");
    var s= "<table>";
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
    js_write_char: function(x, y, c) {
        var cell = document.getElementById("c" + y + "_" + x);
        cell.innerHTML = String.fromCharCode(c);
    },
    js_key_available: function () {
        return lastKey;
    },
    js_read_key: function () {
        var k = lastKey;
        lastKey = 0;
        return k;
    }
});
