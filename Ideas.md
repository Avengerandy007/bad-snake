#Use 2d vector for direction.
Vect2 dir = (0, 1); # up

#Each tick move N amount of pixels in direction.

Vect2 pos;
int speed = 50;

func Update(){
    while (running){
        func Move(){
            pos += speed * dir; 
        }
        switch(key){
            case left:
                dir = (-1, 0);
                break;
            case right:
                dir = (1, 0);
                break;
            case up:
                dir = (0, 1);
                break;
            case down:
                dir = (0, -1);
                break;
        }
    }
}
