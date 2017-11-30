let connection;
function setup()
{
    createCanvas(640, 480);
    rect(0, 0, 639, 479);
    frameRate(30);
    strokeWeight(3);
    connection = new signalR.HubConnection('http://localhost:5000/draw');
    connection.on('draw', function (prev_x, prev_y, x, y)
    {
        drawLineonReceived(prev_x, prev_y, x, y)
    });
    connection.start();
}
let past_x = 0;
let past_y = 0;

function draw()
{
    if (mouseIsPressed)
    {
        drawLineonSelf();
        past_x = mouseX;
        past_y = mouseY;
    }
}
function mousePressed()
{
    past_x = mouseX;
    past_y = mouseY;
}
function drawLineonSelf() 
{
    if (past_x != mouseX || past_y != mouseY)
    {
        line(mouseX, mouseY, past_x, past_y);
        connection.invoke('draw', past_x, past_y, mouseX, mouseY);
    }
}
function drawLineonReceived(x1, y1, x2, y2)
{
    line(x1, y1, x2, y2);
}