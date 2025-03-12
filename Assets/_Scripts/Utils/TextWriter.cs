using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class TextWriter : MonoBehaviour {

    private static TextWriter instance;

    private readonly List<TextWriterSingle> textWriterSingleList = new List<TextWriterSingle>();

    private void Awake() {
        instance = this;
    }

    public static TextWriterSingle AddWriter_Static(TextMeshPro textMeshPro, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete) {
        if (removeWriterBeforeAdd) {
            instance.RemoveWriter(textMeshPro);
        }
        return instance.AddWriter(textMeshPro, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    private TextWriterSingle AddWriter(TextMeshPro textMeshPro, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete) {
        TextWriterSingle textWriterSingle = new TextWriterSingle(textMeshPro, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }
    public static void RemoveWriter_Static(TextMeshPro textMeshPro) {
        instance.RemoveWriter(textMeshPro);
    }
    private void RemoveWriter(TextMeshPro textMeshPro) {
        for (int i = 0; i < textWriterSingleList.Count; i++) {
            if (textWriterSingleList[i].GetTextMeshPro() == textMeshPro) {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update() {
        for (int i = 0; i < textWriterSingleList.Count; i++) {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance) {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }
    [Button]
    void count(){
        Debug.Log(textWriterSingleList.Count);
    }

    /*
     * Represents a single TextWriter instance
     * */
    
}
public class TextWriterSingle{
    private TextMeshPro textMeshPro;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters;
    private Action onComplete;

    public TextWriterSingle(TextMeshPro textMeshPro, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete) {
        this.textMeshPro = textMeshPro;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        this.onComplete = onComplete;
        characterIndex = 0;
    }

    // Returns true on complete
    public bool Update() {
        timer -= Time.deltaTime;
        while (timer <= 0f) {
            // Display next character
            timer += timePerCharacter;
            characterIndex++;
            string text = textToWrite.Substring(0, characterIndex);
            if (invisibleCharacters) {
                text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
            }

            textMeshPro.SetText(text);
            

            if (characterIndex >= textToWrite.Length) {
                if (onComplete != null) onComplete();
                return true;
            }
        }

        return false;
    }

    public TextMeshPro GetTextMeshPro() {
        return textMeshPro;
    }
    
    public bool IsActive() {
        return characterIndex < textToWrite.Length;
    }

    public void WriteAllAndDestroy() {

        textMeshPro.SetText(textToWrite);
        characterIndex = textToWrite.Length;
        if (onComplete != null) onComplete();
        TextWriter.RemoveWriter_Static(textMeshPro);
    }
}
