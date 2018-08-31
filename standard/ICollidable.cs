
namespace ray1.standard
{
    // todo ambient - color in shadow
    // todo diffuse - color under white light
    // todo emissive - color object gives off
    // todo specular - color object gives off on shiny surface (I think there's an angle at which this occurs at)

    public interface ICollidable
    {
        Point Collides( Line line );
    }
}
