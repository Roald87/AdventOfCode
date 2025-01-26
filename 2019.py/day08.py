from more_itertools import chunked

with open("day08.txt") as f:
    image = f.readline().strip()

layer_0_counts = [
    (i, layer.count("0")) for i, layer in enumerate(chunked(image, 25 * 6, strict=True))
]
layer_0_counts = sorted(layer_0_counts, key=lambda x: x[1])

print(
    image[layer_0_counts[0][0] * 25 * 6 : (layer_0_counts[0][0] + 1) * 25 * 6].count(
        "1"
    )
    * image[layer_0_counts[0][0] * 25 * 6 : (layer_0_counts[0][0] + 1) * 25 * 6].count(
        "2"
    )
)

reconstructed_image = ""
for i in range(25 * 6):
    for pixel in image[i :: 25 * 6]:
        if int(pixel) < 2:
            reconstructed_image += pixel
            break

for line in chunked(reconstructed_image, 25, strict=True):
    print("".join(line).replace("0", " "))
