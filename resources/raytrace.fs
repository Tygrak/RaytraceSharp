#version 330

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec4 fragColor;

// Output fragment color
out vec4 finalColor;

uniform vec3 cameraPosition;
uniform vec3 cameraTarget;
uniform vec3 viewCenter;
uniform mat4 cameraInverse;
uniform float iTime;
uniform vec2 iResolution;

struct Ray {
    vec3 origin;
    vec3 dir;
};

struct HitInfo {
    bool hit;
    float d;
    vec3 p;
    vec3 n;
};

HitInfo RaySphere(Ray ray, vec3 sphereCentre, float sphereRadius) {
    HitInfo hitInfo;
    vec3 offsetRayOrigin = ray.origin - sphereCentre;
    float a = dot(ray.dir, ray.dir);
    float b = 2 * dot(offsetRayOrigin, ray.dir);
    float c = dot(offsetRayOrigin, offsetRayOrigin) - sphereRadius * sphereRadius;
    float discriminant = b * b - 4 * a * c;
    if (discriminant >= 0) {
        float dst = (-b - sqrt(discriminant)) / (2 * a);

        if (dst >= 0.0001) {
            hitInfo.hit = true;
            hitInfo.d = dst;
            hitInfo.p = ray.origin + ray.dir * dst;
            hitInfo.n = normalize(hitInfo.p - sphereCentre);
        }
    }
    return hitInfo;
}


Ray getCameraRay(vec2 offset) {
    vec3 origin = cameraPosition;
    vec3 lookAt = cameraTarget;
    //vec3 origin = vec3(0.0, 0.5, -6.0);
    //vec3 lookAt = vec3(0.0, 0.0, 0.0);

    vec2 uv = (gl_FragCoord.xy + offset) / iResolution.xy - .5;
    uv.y *= iResolution.y / iResolution.x;

    vec3 iu = vec3(0., 1., 0.);

    vec3 iz = normalize(lookAt - origin);
    vec3 ix = normalize(cross(iz, iu));
    vec3 iy = cross(ix, iz);

    vec3 direction = normalize( uv.x * ix + uv.y * iy + .85 * iz );

    return Ray(origin, direction);
}

void main() {
    vec2 uv = gl_FragCoord.xy/iResolution;
    vec3 result = vec3(gl_FragCoord.xy/iResolution, sin(iTime));
	//vec2 uv = (2.0*gl_FragCoord.xy / resolution.xy - 1.0) * vec2(aspect_ratio, 1.0);

	vec3 origin = cameraPosition;
    Ray ray = getCameraRay(vec2(0.0, 0.0));
    HitInfo hit = RaySphere(ray, vec3(0.0), 1.0);
    if (hit.hit) {
        finalColor = vec4(hit.n, 1.0);
    } else {
        finalColor = vec4(result, 1.0);
    }
}
