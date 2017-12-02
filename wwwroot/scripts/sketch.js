let connection;
function setup()
{
    createCanvas(640, 480);
    frameRate(30);
    strokeWeight(3);
    background(255, 255, 255);
    let button = createButton('ぜんしょうきょ');
    button.mousePressed(allDelete)
    connection = new signalR.HubConnection('/draw');
    connection.on('draw', function (prev_x, prev_y, x, y)
    {
        drawLineonReceived(prev_x, prev_y, x, y)
    });
    connection.on('init', function (data)
    {
        initialCoordinates = JSON.parse(data);
        console.log(initialCoordinates);
        initialCoordinates.forEach(element =>
        {
            drawLineonReceived(element["PreviousX"], element["PreviousY"], element["NewX"], element["NewY"]);
        });
    });
    connection.on('alldelete', function (data)
    {
        background(255, 255, 255);
        rect(0, 0, 639, 479);

    });
    connection.start();
    rect(0, 0, 639, 479);
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
function allDelete()
{
    connection.invoke('alldelete');
}