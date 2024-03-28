using System;
using System.Collections.Generic;

namespace Assign03.Data;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public string? Images { get; set; }

    public string? Text { get; set; }
}
