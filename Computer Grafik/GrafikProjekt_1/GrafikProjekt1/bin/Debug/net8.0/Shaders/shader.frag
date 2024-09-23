#version 330
out vec4 outputColor;
in vec2 texCoord;
uniform sampler2D texture0;
uniform sampler2D texture1;
uniform float time;

void main()
{

vec2 offset = texCoord - vec2(0.5, 0.5);
vec2 rot = vec2(cos(time) * offset.x + -sin(time) * offset.y, sin(time) * offset.x + cos(time) * offset.y);
vec2 offset2 = rot + vec2(0.5, 0.5);

vec4 src = texture(texture1, offset2);
vec4 dst = texture(texture0, texCoord);


outputColor = src * src.a + dst * (1 - src.a);
 //outputColor = vec4(rot, 0, 1);
}

