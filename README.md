# Kuri RVIZ
 NRI SVTE Signal Design for Non-Expert Users
 
  ----

## Setup

- Change line 88 of `ColorPicker.cs` to 
    ```
    public Color CustomColor;
    ```
- Change lines 1872-1873 in `BoundingBox.cs` and lines 905-906 in `BoundsControl.cs` from
    ```
    KeyValuePair<Transform, Collider> colliderByTransform;
    KeyValuePair<Transform, Bounds> rendererBoundsByTransform;
    ``` 
    to
    ```
    KeyValuePair<Transform, Collider> colliderByTransform = default;
    KeyValuePair<Transform, Bounds> rendererBoundsByTransform = default;
    ```

## Style Guide

- Follow Microsoft's [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
