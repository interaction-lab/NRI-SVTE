# DEPRECATED - due to an lfs issue, this repo has been more [here](https://github.com/interaction-lab/NRI-SVTE-ACTIVE-PROXEMICS)

# Kuri RVIZ
 NRI SVTE Signal Design for Non-Expert Users
 
  ----

## Study Commits
Thomas R. Groechel*, Allison O'Connell*, Massimiliano Nigro*, and Maja J. Matarić. "Reimagining RViz: Multidimensional Augmented Reality Robot Signal Design", In 2022 IEEE International Symposium on Robot and Human Interactive Communication (RO-MAN 2022), Aug-2022. - [77610bae15ae9a2fb632c31f9b26150ffe8e258a](https://github.com/interaction-lab/NRI-SVTE/commit/77610bae15ae9a2fb632c31f9b26150ffe8e258a), [Video](https://youtu.be/Xw2_kHyN-xA)

## Thanks
We want to thank 
- [The Kiwi Coder](https://thekiwicoder.com/behaviour-tree-editor/) for their free behavior tree unitypackage.
- [KDTrees](https://github.com/viliwonka/KDTree) for their package on kd trees
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
